namespace HotelUp.Employee.Shared.Exceptions;

public class TokenException : Exception
{
    public TokenException(string message) : base(message)
    {
    }
}