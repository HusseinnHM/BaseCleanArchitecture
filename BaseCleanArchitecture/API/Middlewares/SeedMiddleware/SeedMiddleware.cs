using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BaseCleanArchitecture.API.Middlewares.SeedMiddleware;

public static class SeedMiddleware
{
    
    public static async Task<IApplicationBuilder> MigrationAsync<TContext>(this IApplicationBuilder builder,
        bool deleteDbIfFailure = false) where TContext : DbContext
    {
        
        var serviceProvider = builder.ServiceProvider();
        var logger = serviceProvider.GetRequiredService<ILogger<TContext>>();
        var context = serviceProvider.GetRequiredService<TContext>();
        try
        {
            await context.Database.MigrateAsync();
            logger.LogInformation("Migrate is done");
        }
        catch (Exception e)
        {
            logger.LogError(e, "Failed while applies migrations");
            if (deleteDbIfFailure)
            {
                logger.LogInformation("Start delete db and re-applies migrations");
                await context.Database.EnsureCreatedAsync();
                await context.Database.EnsureDeletedAsync();
                await context.Database.MigrateAsync();
            }
        }

        return builder;
    }

    public static async Task<IApplicationBuilder> MigrationAsync<TContext>(this IApplicationBuilder builder,
        Func<TContext, IServiceProvider, Task> seed, bool deleteDbIfFailure = false) where TContext : DbContext
    {
        var serviceProvider = builder.ServiceProvider();
        var logger = serviceProvider.GetRequiredService<ILogger<TContext>>();
        var context = serviceProvider.GetRequiredService<TContext>();
        await using var db = await context.Database.BeginTransactionAsync();
        try
        {
            await context.Database.MigrateAsync();
            logger.LogInformation("Migrate is done");
            
            await seed(context, serviceProvider);
            logger.LogInformation("Seed is done");
            await db.CommitAsync();

        }
        catch (Exception e)
        {
            await db.RollbackAsync();
            logger.LogError(e, "Failed while applies migrations/seed data");
            
            if (deleteDbIfFailure)
            {
                await context.Database.EnsureCreatedAsync();
                await context.Database.EnsureDeletedAsync();
                await context.Database.MigrateAsync();
                await seed(context, serviceProvider);
            }
        }

        return builder;
    }

    public static async Task<IApplicationBuilder> MigrationAsync<TContext>(this IApplicationBuilder builder,
        Action<TContext, IServiceProvider> seed, bool deleteDbIfFailure = false) where TContext : DbContext
    {
        var serviceProvider = builder.ServiceProvider();
        var logger = serviceProvider.GetRequiredService<ILogger<TContext>>();
        var context = serviceProvider.GetRequiredService<TContext>();
        await using var db = await context.Database.BeginTransactionAsync();
        try
        {
            await context.Database.MigrateAsync();
            logger.LogInformation("Migrate is done");

            seed(context, serviceProvider);
            logger.LogInformation("Seed is done");
            await db.CommitAsync();
        }
        catch (Exception e)
        {
            await db.RollbackAsync();
            logger.LogError(e, "Failed while applies migrations/seed data");
            if (deleteDbIfFailure)
            {
                await context.Database.EnsureCreatedAsync();
                await context.Database.EnsureDeletedAsync();
                await context.Database.MigrateAsync();
                seed(context, serviceProvider);
            }
        }

        return builder;
    }

    public static async Task<IApplicationBuilder> SeedAsync<TContext>(this IApplicationBuilder builder,
        Func<TContext, IServiceProvider, Task> seed) where TContext : DbContext
    {
        var serviceProvider = builder.ServiceProvider();
        var logger = serviceProvider.GetRequiredService<ILogger<TContext>>();
        var context = serviceProvider.GetRequiredService<TContext>();
        try
        {
            await seed(context, serviceProvider);
            logger.LogInformation("Seed is done");
        }
        catch (Exception e)
        {
            logger.LogError(e, "Failed while seed data");
        }
        return builder;
    }

    public static IApplicationBuilder Seed<TContext>(this IApplicationBuilder builder,
        Action<TContext, IServiceProvider> seed) where TContext : DbContext
    {
        var serviceProvider = builder.ServiceProvider();
        var logger = serviceProvider.GetRequiredService<ILogger<TContext>>();
        var context = serviceProvider.GetRequiredService<TContext>();
        try
        {
            seed(context, serviceProvider);
            logger.LogInformation("Seed is done");
        }
        catch (Exception e)
        {
            logger.LogError(e, "Failed while seed data");
        }
        return builder;
    }

    private static IServiceProvider ServiceProvider(this IApplicationBuilder builder) => builder
        .ApplicationServices
        .GetRequiredService<IServiceScopeFactory>()
        .CreateScope()
        .ServiceProvider;

    

}