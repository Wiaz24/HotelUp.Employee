using Amazon;
using Amazon.CognitoIdentityProvider;
using Amazon.Extensions.NETCore.Setup;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace HotelUp.Employee.Persistence.Cognito;

public static class Extensions
{
    private const string SectionName = "AWS:Cognito";
    
    public static IServiceCollection AddCognito(this IServiceCollection services)
    {
        services.AddOptions<CognitoOptions>()
            .BindConfiguration(SectionName)
            .ValidateDataAnnotations()
            .ValidateOnStart();
        
        var options = services.BuildServiceProvider().GetRequiredService<IOptions<CognitoOptions>>().Value;
        services.AddAWSService<IAmazonCognitoIdentityProvider>(options: new AWSOptions()
        {
            Profile = options.Profile,
            Region = RegionEndpoint.GetBySystemName(options.Region)
        });
        return services;
    }
}