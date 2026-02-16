namespace RegistroNF.API.Core.Contracts.Service
{
    public interface IEmailService
    {
        Task SendEmailAsync(string from, string to, string body);
    }
}
