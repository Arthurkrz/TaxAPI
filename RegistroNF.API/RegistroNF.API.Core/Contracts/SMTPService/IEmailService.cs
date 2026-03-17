using RegistroNF.API.Core.Common;

namespace RegistroNF.API.Core.Contracts.SMTPService
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailMessage message);
    }
}
