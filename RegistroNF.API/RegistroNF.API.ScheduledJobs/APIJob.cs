using Microsoft.Extensions.Logging;
using RegistroNF.API.Core.Contracts.Service;
using RegistroNF.API.Core.Contracts.SMTPService;

namespace RegistroNF.API.ScheduledJobs
{
    public class APIJob
    {
        private readonly IEmpresaService _empresaService;
        private readonly IEmailService _emailService;
        private readonly ILogger<APIJob> _logger;

        public APIJob(IEmpresaService empresaService, IEmailService emailService, ILogger<APIJob> logger)
        {
            _empresaService = empresaService;
            _emailService = emailService;
            _logger = logger;
        }

        public async Task ExecuteAsync()
        {
            var data = DateTime.Now.AddMonths(-1);
            var parciais = await _empresaService.GetEmpresasIncompletasAsync(data.Month, data.Year);

            // envio email
        }
    }
}
