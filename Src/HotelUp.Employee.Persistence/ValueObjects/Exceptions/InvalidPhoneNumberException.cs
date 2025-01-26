using HotelUp.Employee.Shared.Exceptions;

namespace HotelUp.Employee.Persistence.ValueObjects.Exceptions;

public class InvalidPhoneNumberException : BusinessRuleException
{
    public InvalidPhoneNumberException(string? message) : base($"Phone number is invalid. {message}")
    {
    }
}