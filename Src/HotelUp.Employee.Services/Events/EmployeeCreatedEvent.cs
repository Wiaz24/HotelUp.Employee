using HotelUp.Employee.Persistence.Const;

namespace HotelUp.Employee.Services.Events;

public record EmployeeCreatedEvent
{
    public required Guid Id { get; init; }
    public required EmployeeType Role { get; init; }
}