using Aspire.Hosting.Yarp;
using Aspire.Hosting.Yarp.Transforms;

var builder = DistributedApplication.CreateBuilder(args);

// My Environment
var postgres = builder.AddPostgres("postgres")
    .WithLifetime(ContainerLifetime.Persistent)
    .WithImage("postgres:17.5"); // You can use "custom" images too.


// My External Dev Environment

//var db2 = builder.AddConnectionString("db2");


//var someExternalService = builder.AddExternalService("todos", "https://jsonplaceholder.typicode.com");

var identity = builder.AddContainer("identity", "ghcr.io/navikt/mock-oauth2-server:3.0.1")
    .WithLifetime(ContainerLifetime.Persistent)
    .WithHttpEndpoint(9069, 8080);

// My Apis

var ordersDb = postgres.AddDatabase("orders");

var ordersApi = builder.AddProject<Projects.Orders_Api>("ordersapi")
    .WithReference(ordersDb)
    .WithEnvironment("identity", () => identity.GetEndpoint("http").Url)
    .WaitFor(ordersDb)
    .WaitFor(identity);

var productsApi = builder.AddProject<Projects.Products_Api>("products-api");

var usersApi = builder.AddProject<Projects.Users_Api>("users-api");
// To get into my app, you go to the endpoint for this - and it directs the requests to the appropriate service

var gateway = builder.AddYarp("gateway")
    .WithReference(ordersApi)
    .WithReference(productsApi)
    .WithReference(usersApi)
    .WithConfiguration(routes =>
    {
        routes.AddRoute("/users", usersApi).WithTransformPathRemovePrefix("/users");
        routes.AddRoute("/products", productsApi).WithTransformPathRemovePrefix("/products");
        routes.AddRoute("/{*catchall}", ordersApi);
    });



builder.Build().Run();
