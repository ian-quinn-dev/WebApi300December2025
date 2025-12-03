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

app.MapGet("/employees/{id:int}/performance-evals", (int id) =>
{
    List<string> lastFiveEvals = [
       "A", "A", "C", "D", "B"
       ];

    return TypedResults.Ok(new { employeeId = id, lastFiveEvals });
});

app.Run();

