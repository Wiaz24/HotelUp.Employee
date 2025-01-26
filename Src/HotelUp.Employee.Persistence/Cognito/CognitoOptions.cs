namespace HotelUp.Employee.Persistence.Cognito;

public class CognitoOptions
{
    public required string Region { get; init; }
    public required string Profile { get; init; }
    public required string UserPoolId { get; init; }
}