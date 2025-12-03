using EmployeesCommon;
using System.ComponentModel.DataAnnotations;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.MapPost("/employees", (NewEmployeeCreateRequest request) =>
{

    // do whatever "improvements" you want. 
    var response = new NewEmployeeInfo(new Random().Next(100, 1000), request.Name.ToUpper(), request.Phone, request.EyeColor);
    return TypedResults.Ok(response);
});

app.Run();

public record NewEmployeeCreateRequest() : EmployeeCreateRequest
{
    [Required, MinLength(5), MaxLength(5)]
    public string EyeColor { get; set; } = string.Empty;
}

public record NewEmployeeInfo(int Id, string Name, string Phone, string EyeColor) : EmployeeInfo(Id, Name, Phone);