using HotelUp.Employee.Shared.Exceptions;

namespace HotelUp.Employee.Persistence.ValueObjects.Exceptions;

public class InvalidEmailException : BusinessRuleException
{
    public InvalidEmailException(string? message) : base($"Email is invalid. {message}")
    {
    }
}