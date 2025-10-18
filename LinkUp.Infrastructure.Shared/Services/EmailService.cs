using LinkUp.Core.Applicacion.Dtos.Email;
using LinkUp.Core.Domain.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;

namespace LinkUp.Infrastructure.Shared.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSetting _Email;
        private readonly ILogger<EmailService> _Logger;

        public EmailService(IOptions<EmailSetting> Email, ILogger<EmailService> Logger)
        {
            _Email = Email.Value;
            _Logger = Logger;
        }

        public async Task SendEmailAsync(EmailRequest dto)
        {
            try
            {
                dto.ToRange?.Add(dto.To ?? "");

                MimeMessage Email = new()
                {
                    Sender = MailboxAddress.Parse(_Email.EmailFrom),
                    Subject = dto.Subject
                };

                foreach (var item in dto.ToRange ?? [])
                {
                    Email.To.Add(MailboxAddress.Parse(item));
                }

                BodyBuilder bodyBuilder = new()
                {
                    HtmlBody = dto.HtmlBody
                };

                Email.Body = bodyBuilder.ToMessageBody();

                using MailKit.Net.Smtp.SmtpClient smtpClient = new();
                await smtpClient.ConnectAsync(_Email.SmtpHost, _Email.SmtpPort, MailKit.Security.SecureSocketOptions.StartTls);
                await smtpClient.AuthenticateAsync(_Email.SmtpFrom, _Email.SmtpPass);
                await smtpClient.SendAsync(Email);
                await smtpClient.DisconnectAsync(true);

            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, "An excepcion ocurred {Exception}", ex);
            }
        }
    }
}
