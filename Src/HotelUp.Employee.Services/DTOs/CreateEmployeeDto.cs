using System.ComponentModel;
using HotelUp.Employee.Persistence.Const;

namespace HotelUp.Employee.Services.DTOs;

public record CreateEmployeeDto
{
    [DefaultValue("John")]
    public required string FirstName { get; init; }
    
    [DefaultValue("Cramer")]
    public required string LastName { get; init; }
    
    [DefaultValue("example@email.com")]
    public required string Email { get; init; }
    
    [DefaultValue("+48123456789")]
    public required string PhoneNumber { get; init; }
    public required EmployeeType Role { get; init; }
}