using HotelUp.Employee.Persistence.Const;
using HotelUp.Employee.Persistence.ValueObjects;

namespace HotelUp.Employee.Persistence.Entities;

public class Employee
{
    public required Guid Id { get; set; }
    public required FirstName FirstName { get; init; }
    public required LastName LastName { get; init; }
    public required Email Email { get; init; }
    public required PhoneNumber PhoneNumber { get; init; }
    public required EmployeeType Role { get; init; }
}