using AppHost;
using Projects;
using Scalar.Aspire;

var builder = DistributedApplication.CreateBuilder(args);

#region Local Development Environment Setup

#region PostgreSQL

var postgresPassword = builder.AddParameter("postgres-password", true)
    .WithDescription("Postgres Password for Development Environment");
var postgresUserName = builder.AddParameter("postgres-user", true)
    .WithDescription("Postgres User for Development Environment");
var postGresServer = builder.AddPostgres("software-center-postgres", postgresUserName, postgresPassword, 5432)
    .WithImage("postgres:17.5-bullseye")
    .WithContainerName("dev-postgres")
    .WithDataVolume(isReadOnly: false)
    .WithHostPort(5433)
    .WithLifetime(ContainerLifetime.Persistent)
    .WithChildRelationship(postgresUserName)
    .WithChildRelationship(postgresPassword);

#endregion

#region Identity

// docs: https://github.com/navikt/mock-oauth2-server
//  http://localhost:9069/software/.well-known/openid-configuration
//  http://localhost:9069/software/debugger
var identity = builder.AddContainer("identity", "ghcr.io/navikt/mock-oauth2-server:3.0.1")
    .WithHttpEndpoint(9069, 8080) // Expose port 9069 on host to 8080 in container
    .WithBindMount("./MockOauth2/",
        "/app/resources/software/") // Mount local folder to container, contains config and login template
    .WithLifetime(ContainerLifetime.Persistent) // Keep container and data between runs
    .WithEnvironment("JSON_CONFIG_PATH", "/app/resources/software/settings/config.json")
    .WithGetTokenForMary() // These are in AppHostExtensions - running them will get the tokens for these users and output them to the console
    .WithGetTokenForAlice()
    .WithGetTokenForEarl();

#endregion

#endregion

#region Services

#region Catalog

var catalogDb = postGresServer.AddDatabase("catalog");
var catalogApi = builder.AddProject<Catalog_Api>("catalogapi")
    .WithReference(catalogDb)
    .WaitFor(catalogDb)
    .WithIdentityOpenIdBearer(identity)
    .WithIdentityOpenIdAuthority(identity)
    .WithSharedLoggingLevels();

#endregion

#region Vendors

var vendorsDb = postGresServer.AddDatabase("vendors");
var vendorsApi = builder.AddProject<Vendors_Api>("vendorsapi")
    .WithReference(vendorsDb)
    .WaitFor(vendorsDb)
    .WithIdentityOpenIdBearer(identity)
    .WithIdentityOpenIdAuthority(identity)
    .WithSharedLoggingLevels();

#endregion

#region Backend For Frontend

var angularFrontend = builder
    .AddNpmApp("angular-frontend", "../../Services/BackendForFrontend/AngularFrontend/", "start:aspire")
    .WithEnvironment("PORT", "4250")
    .WithHttpEndpoint(env: "PORT")
    .WithExternalHttpEndpoints()
    .PublishAsDockerFile()
    .WithNpmPackageInstallation(); // if this hasn't had NPM install done, do it.

var angularDevKey = "bff-angular-dev";
var angularDev = builder.AddParameter(angularDevKey); // Either "dev" or "prod"

var bffApi = builder.AddProject<BackendForFrontend_Api>("Gateway")
    .WithReference(catalogApi)
    .WithReference(vendorsApi)
    .WithReference(angularFrontend)
    .WithEnvironment("FRONT_END", angularDev)
    .WithExternalHttpEndpoints()
    .WithChildRelationship(catalogApi)
    .WithChildRelationship(vendorsApi)
    .WithChildRelationship(angularFrontend)
    .WithIdentityOpenIdAuthority(identity)
    .WaitFor(catalogApi)
    .WaitFor(vendorsApi)
    .WithSharedLoggingLevels();

var scalarApis = builder.AddScalarApiReference("scalar-apis", 9561, options =>
    {
        options.DisableDefaultProxy();
        options.PreferHttpsEndpoint();
        options.PersistentAuthentication = true;
        options.AllowSelfSignedCertificates();
        options.AddPreferredSecuritySchemes("oauth2")
            .AddAuthorizationCodeFlow("oauth2",
                flow =>
                {
                    flow.WithClientId("aspire-client")
                        .WithClientSecret("super-secret")
                        .WithSelectedScopes("openid", "profile", "email", "roles");
                });

        options.WithOpenApiRoutePattern("/openapi/{documentName}.json");
    })
    .WaitFor(identity)
    .WithParentRelationship(bffApi)
    .WithExplicitStart();

scalarApis.WithApiReference(vendorsApi, options => options.AddDocument("vendors.v1", "Vendors API"));
scalarApis.WithApiReference(catalogApi, options => { options.AddDocument("catalog.v1", "Catalog API"); });


#endregion

#endregion

builder.Build().Run();