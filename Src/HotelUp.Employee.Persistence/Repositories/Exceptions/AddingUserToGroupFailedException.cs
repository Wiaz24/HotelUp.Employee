using HotelUp.Employee.Shared.Exceptions;

namespace HotelUp.Employee.Persistence.Repositories.Exceptions;

public class AddingUserToGroupFailedException : BusinessRuleException
{
    public AddingUserToGroupFailedException(string statusCode) : base($"Adding user to group failed with status code: {statusCode}")
    {
    }
}