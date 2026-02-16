using RegistroNF.API.Core.Contracts.Service;

namespace RegistroNF.API.Services
{
    public class EmailService : IEmailService
    {
        public Task SendEmailAsync(string from, string to, string body)
        {
            throw new NotImplementedException();
        }
    }
}
