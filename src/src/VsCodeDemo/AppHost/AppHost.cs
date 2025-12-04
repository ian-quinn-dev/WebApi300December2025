var builder = DistributedApplication.CreateBuilder(args);
builder.AddProject<Projects.SomeApi>("api");

builder.Build().Run();
