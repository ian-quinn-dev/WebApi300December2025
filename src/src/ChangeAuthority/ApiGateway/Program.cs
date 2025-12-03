var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults(); // aspire

builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"))
    .AddServiceDiscoveryDestinationResolver(); // is so that it can resolve URLs given to it from AppHost.


var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
app.MapReverseProxy();  // Add This.

app.Run();
