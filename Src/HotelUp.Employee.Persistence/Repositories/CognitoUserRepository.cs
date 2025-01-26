using System.Net;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using HotelUp.Employee.Persistence.Cognito;
using HotelUp.Employee.Persistence.Const;
using HotelUp.Employee.Persistence.Repositories.Exceptions;
using HotelUp.Employee.Persistence.ValueObjects;
using Microsoft.Extensions.Options;

namespace HotelUp.Employee.Persistence.Repositories;

public class CognitoUserRepository : IUserRepository
{
    private readonly IAmazonCognitoIdentityProvider _cognitoService;
    private readonly CognitoOptions _cognitoOptions;

    public CognitoUserRepository(IOptionsSnapshot<CognitoOptions> cognitoOptions, 
        IAmazonCognitoIdentityProvider cognitoService)
    {
        _cognitoService = cognitoService;
        _cognitoOptions = cognitoOptions.Value;
    }

    public async Task<Guid> SignUpAsync(Email email, EmployeeType employeeType)
    {
        var request = new AdminCreateUserRequest
        {
            UserPoolId = _cognitoOptions.UserPoolId,
            Username = email.Value,
            UserAttributes = new List<AttributeType>
            {
                new AttributeType
                {
                    Name = "email",
                    Value = email.Value
                }
            }

        };
        var response = await _cognitoService.AdminCreateUserAsync(request);
        if (response.HttpStatusCode != HttpStatusCode.OK)
        {
            throw new RegistrationFailedException(response.HttpStatusCode.ToString());
        }
        var sub = response.User.Attributes.First(x => x.Name == "sub").Value;
        if (!Guid.TryParse(sub, out var userId))
        {
            throw new CannotParseUserSubException(sub);
        }
        
        var groupRequest = new AdminAddUserToGroupRequest
        {
            UserPoolId = _cognitoOptions.UserPoolId,
            Username = email.Value,
            GroupName = $"{employeeType.ToString()}s"
        };
        var groupResponse = await _cognitoService.AdminAddUserToGroupAsync(groupRequest);
        if (groupResponse.HttpStatusCode != HttpStatusCode.OK)
        {
            throw new AddingUserToGroupFailedException(groupResponse.HttpStatusCode.ToString());
        }
        return userId;
    }
}