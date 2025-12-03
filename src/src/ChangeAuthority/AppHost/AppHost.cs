var builder = DistributedApplication.CreateBuilder(args);

var employeesApi = builder.AddProject<Projects.Employees_Api>("employeesapi");
var employeeManagementApi = builder.AddProject<Projects.EmployeeManager_Api>("employeesapimanagement");

var updatedEmployeeApi = builder.AddProject<Projects.UpdatedAndNewEmployeeApi>("updatedandnewemployeeapi");


builder.AddProject<Projects.ApiGateway>("apigateway")
    .WithReference(employeesApi)
    .WithReference(employeeManagementApi)
    .WithReference(updatedEmployeeApi);


builder.Build().Run();
