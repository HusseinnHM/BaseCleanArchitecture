using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sample.Application.Core.Abstractions.Email;
using Sample.Infrastructure.Email;
using Sample.Infrastructure.Email.Settings;

namespace Sample.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        
        services.Configure<MailSettings>(configuration.GetSection(MailSettings.SettingsKey));

        services.AddScoped<IEmailService, EmailService>();
        
        return services;
    }
}