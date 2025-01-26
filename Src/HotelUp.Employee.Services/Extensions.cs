using HotelUp.Employee.Services.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HotelUp.Employee.Services;

public static class Extensions
{
    public static IServiceCollection AddServiceLayer(this IServiceCollection services)
    {
        services.AddApplicationServices();
        return services;
    }
}