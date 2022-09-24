using Sample.Application.Core.Contracts.Email;

namespace Sample.Application.Core.Abstractions.Email;

public interface IEmailService
{
    System.Threading.Tasks.Task SendEmailAsync(SendEmailRequest sendEmailRequest);
}