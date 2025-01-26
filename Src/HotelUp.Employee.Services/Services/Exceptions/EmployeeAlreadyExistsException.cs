using HotelUp.Employee.Shared.Exceptions;

namespace HotelUp.Employee.Services.Services.Exceptions;

public class EmployeeAlreadyExistsException : BusinessRuleException
{
    public EmployeeAlreadyExistsException(string email)
        : base($"Employee with email {email} already exists.")
    {
    }
}