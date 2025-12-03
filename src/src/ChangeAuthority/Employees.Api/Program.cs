using Employees.Api;
using EmployeesCommon;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using System.ComponentModel.DataAnnotations;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();
// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi(c =>
{
    c.AddDocumentTransformer<CustomDocumentTransformer>();
});
builder.Services.AddValidation();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();



app.MapGet("/employees", () =>
{
    List<EmployeeInfo> employees = [
        new EmployeeInfo(1, "Bob Smith", "555-1212"),
        new EmployeeInfo(2, "Renee Jones", "555-1212")
        ];
    return TypedResults.Ok(employees);
});

app.MapGet("/employees/{id:int}", (int id) =>
{
   
    return new EmployeeInfo(id, "Bob Jones", "555-1212");
});



app.MapPost("/employees", async (EmployeeCreateRequest request) =>
{
var dateWeSaidToBeOffThisApi = new DateTime(2025, 12, 1);
    await Task.Delay(1000 * (DateTime.Now - dateWeSaidToBeOffThisApi).Days); // only sort of joking here
    var response = new EmployeeInfo(new Random().Next(100, 1000), request.Name, request.Phone);
    var newResponse = new
    {
        id = response.Id,
        name = response.Name,
        phone = response.Phone,
        warningMessage = "We are going to retire this endpoint soon!, as per our email"
    };
    return TypedResults.Ok(newResponse);
});



app.MapDefaultEndpoints();
app.Run();


