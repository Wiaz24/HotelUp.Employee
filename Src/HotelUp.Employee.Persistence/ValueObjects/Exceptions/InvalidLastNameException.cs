using HotelUp.Employee.Shared.Exceptions;

namespace HotelUp.Employee.Persistence.ValueObjects.Exceptions;

public class InvalidLastNameException : BusinessRuleException
{
    public InvalidLastNameException(string value) : base($"Invalid last name: {value}")
    {
    }
}