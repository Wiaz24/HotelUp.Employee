using HotelUp.Employee.Shared.Exceptions;

namespace HotelUp.Employee.Persistence.Repositories.Exceptions;

public class RegistrationFailedException : BusinessRuleException
{
    public RegistrationFailedException(string statusCode) : base($"Registration failed with status code: {statusCode}")
    {
    }
}