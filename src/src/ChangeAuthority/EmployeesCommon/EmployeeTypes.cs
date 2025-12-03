using System.ComponentModel.DataAnnotations;

namespace EmployeesCommon;

/// <summary>
/// This is information about an employee
/// </summary>
/// <param name="Id"></param>
/// <param name="Name"></param>
/// <param name="Phone">Their work phone number</param>
public record EmployeeInfo(

    int Id, [property: MaxLength(50), MinLength(3)] string Name, string Phone);


/// <summary>
/// This is used to create an Employee
/// </summary>
public record EmployeeCreateRequest
{
    [Required, MinLength(3), MaxLength(50)]
    public string Name { get; set; } = string.Empty;
    // add a regex or something for phone, whateer
    public string Phone { get; set; } = string.Empty;

}
