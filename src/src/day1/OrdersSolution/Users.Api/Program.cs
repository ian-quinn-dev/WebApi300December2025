var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();


var usersGroup = app.MapGroup("/");

usersGroup.MapGet("/", () => "Big Ole List of Users Here");
usersGroup.MapGet("/{id}", (string id) => "Getting you a user" + id);
app.Run();

