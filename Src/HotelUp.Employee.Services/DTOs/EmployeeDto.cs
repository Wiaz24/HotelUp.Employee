using HotelUp.Employee.Persistence.Const;

namespace HotelUp.Employee.Services.DTOs;

public record EmployeeDto
{
    public required Guid Id { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string Email { get; init; }
    public required string PhoneNumber { get; init; }
    public required EmployeeType Role { get; init; }
    
    public static EmployeeDto FromEntity(Persistence.Entities.Employee employee)
    {
        return new EmployeeDto
        {
            Id = employee.Id,
            FirstName = employee.FirstName.Value,
            LastName = employee.LastName.Value,
            Email = employee.Email.Value,
            PhoneNumber = employee.PhoneNumber.Value,
            Role = employee.Role
        };
    }
}