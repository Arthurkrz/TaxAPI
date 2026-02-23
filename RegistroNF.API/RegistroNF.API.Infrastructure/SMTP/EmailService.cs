using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using RegistroNF.API.Core.Common;
using RegistroNF.API.Core.Contracts.SMTPService;

namespace RegistroNF.API.Services
{
    public class EmailService : IEmailService
    {
        private readonly SMTPSettings _smtpSettings;

        public EmailService(IOptions<SMTPSettings> smtpSettings)
        {
            _smtpSettings = smtpSettings.Value;
        }

        public async Task SendEmailAsync(EmailMessage message)
        {
            MimeMessage mimeMessage = new();
            mimeMessage.From.Add(MailboxAddress.Parse(_smtpSettings.Email));
            mimeMessage.To.AddRange(message.To.Select(e => MailboxAddress.Parse(e)));
            mimeMessage.Subject = message.Subject;

            var bodyBuilder = new BodyBuilder()
            {
                HtmlBody = message.IsHtml ? message.Body : null,
                TextBody = message.IsHtml ? null : message.Body
            };

            mimeMessage.Body = bodyBuilder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_smtpSettings.Host, 
                    _smtpSettings.Port, SecureSocketOptions.StartTls);

                await client.AuthenticateAsync(_smtpSettings.Email, _smtpSettings.Password);

                await client.SendAsync(mimeMessage);
                await client.DisconnectAsync(true);
            }
        }
    }
}
