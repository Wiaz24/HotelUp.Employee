using Microsoft.Extensions.DependencyInjection;

namespace HotelUp.Employee.Persistence.Repositories;

public static class Extensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IUserRepository, CognitoUserRepository>();
        return services;
    }
}