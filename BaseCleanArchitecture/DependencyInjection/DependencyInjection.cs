using System.Reflection;
using BaseCleanArchitecture.Application.Core.Abstractions.Common;
using BaseCleanArchitecture.Application.Core.Abstractions.Data;
using BaseCleanArchitecture.Application.Core.Abstractions.Request;
using BaseCleanArchitecture.Application.Dispatchers.DomainEventDispatcher;
using BaseCleanArchitecture.Application.Dispatchers.RequestDispatcher;
using BaseCleanArchitecture.Domain.Events;
using BaseCleanArchitecture.Domain.Repository;
using BaseCleanArchitecture.Infrastructure.Common;
using Microsoft.Extensions.DependencyInjection;

namespace BaseCleanArchitecture.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddBaseCleanArchitecture<TIContext, TContext>(
        this IServiceCollection services, params string[] assemblyNames)
        where TContext : IDbContext, IUnitOfWork, TIContext
        where TIContext : class, IDbContext 
        => services.AddBaseCleanArchitecture<TIContext, TContext>(assemblyNames.Select(Assembly.Load).ToArray());

    public static IServiceCollection AddBaseCleanArchitecture<TIContext, TContext>(
        this IServiceCollection services, params Assembly[] assemblyNames)
        where TContext : IDbContext, IUnitOfWork, TIContext
        where TIContext : class, IDbContext
    {
        services.AddTransient<IDateTime, DateTimeService>();

        services.AddLogging();
        services.AddScoped<TIContext>(serviceProvider => serviceProvider.GetRequiredService<TContext>());
        services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<TContext>());

        services.AddSingleton<IRequestDispatcher, InMemoryRequestDispatcher>();
        services.AddSingleton<IDomainEventDispatcher, InMemoryDomainEventDispatcher>();

        services.Scan(s => s.FromAssemblies(assemblyNames)
            .AddClasses(c => c.AssignableToAny(typeof(IRequestHandler<,>),typeof(IDomainEventHandler<>),typeof(IRepository<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());
        

        return services;
    }

}