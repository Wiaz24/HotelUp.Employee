using HotelUp.Employee.Shared.Exceptions;

namespace HotelUp.Employee.Persistence.ValueObjects.Exceptions;

public class InvalidFirstNameException : BusinessRuleException
{
    public InvalidFirstNameException(string name) : base($"Invalid first name: {name}")
    {
    }
}