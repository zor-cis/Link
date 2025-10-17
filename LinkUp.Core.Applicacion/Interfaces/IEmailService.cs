using LinkUp.Core.Applicacion.Dtos.Email;

namespace LinkUp.Infrastructure.Shared.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailRequest dto);
    }
}