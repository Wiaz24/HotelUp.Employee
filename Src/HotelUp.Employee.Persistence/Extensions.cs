﻿using HotelUp.Employee.Persistence.EFCore;
using HotelUp.Employee.Persistence.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HotelUp.Employee.Persistence;

public static class Extensions
{
    public static IServiceCollection AddPersistenceLayer(this IServiceCollection services)
    {
        services.AddDatabase();
        services.AddRepositories();
        return services;
    }
}