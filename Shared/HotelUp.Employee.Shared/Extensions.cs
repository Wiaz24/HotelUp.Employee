using HotelUp.Employee.Shared.Auth;
using HotelUp.Employee.Shared.Logging;
using HotelUp.Employee.Shared.Messaging;
using HotelUp.Employee.Shared.SystemsManager;
using HealthChecks.UI.Client;
using HotelUp.Employee.Shared.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.DependencyInjection;

namespace HotelUp.Employee.Shared;

public static class Extensions
{
    public static WebApplicationBuilder AddShared(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<TimeProvider>(TimeProvider.System);
        builder.AddCustomSystemsManagers();
        builder.Services.AddHealthChecks();
        builder.Services.AddAuth(builder.Configuration);
        builder.Services.AddHttpClient();
        builder.Services.AddMessaging();
        builder.Services.AddTransient<ExceptionMiddleware>();
        builder.AddCustomLogging();
        return builder;
    }

    public static IApplicationBuilder UseShared(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionMiddleware>();
        app.UseHealthChecks("/api/employee/_health", new HealthCheckOptions
        {
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });
        app.UseAuth();
        return app;
    }
}