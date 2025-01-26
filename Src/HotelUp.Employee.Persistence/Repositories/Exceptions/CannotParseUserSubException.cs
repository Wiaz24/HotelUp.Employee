namespace HotelUp.Employee.Persistence.Repositories.Exceptions;

public class CannotParseUserSubException : Exception
{
    public CannotParseUserSubException(string sub) : base($"Cannot parse user sub: {sub}")
    {
    }
    
}