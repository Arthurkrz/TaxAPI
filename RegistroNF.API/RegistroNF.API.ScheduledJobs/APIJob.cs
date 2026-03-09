using Microsoft.Extensions.Logging;
using RegistroNF.API.Core.Common;
using RegistroNF.API.Core.Contracts.Repository;
using RegistroNF.API.Core.Contracts.Service;
using RegistroNF.API.Core.Contracts.SMTPService;
using RegistroNF.API.Core.Enum;
using RegistroNF.API.Services.Utilities;

namespace RegistroNF.API.ScheduledJobs
{
    public class APIJob
    {
        private readonly IEmpresaService _empresaService;
        private readonly IEmpresaRepository _empresaRepository;
        private readonly IEmailService _emailService;
        private readonly ILogger<APIJob> _logger;

        public APIJob(IEmpresaService empresaService, IEmpresaRepository empresaRepository, IEmailService emailService, ILogger<APIJob> logger)
        {
            _empresaService = empresaService;
            _empresaRepository = empresaRepository;
            _emailService = emailService;
            _logger = logger;
        }

        public async Task ExecuteAsync()
        {
            var incompletas = await _empresaService.GetEmpresasIncompletasAsync();

            foreach (var empresa in incompletas)
            {
                switch (empresa.Status)
                {
                    case Status.Parcial:
                        empresa.Status = Status.Notificado;

                        var emailNotificado = EmailBuilder.BuildEmailNotificado(empresa);

                        await _empresaRepository.UpdateAsync(empresa);
                        await _emailService.SendEmailAsync(emailNotificado);
                        break;

                    case Status.Notificado:
                        empresa.Status = Status.Bloqueado;

                        var emailBloqueado = EmailBuilder.BuildEmailBloqueado(empresa);

                        await _empresaRepository.UpdateAsync(empresa);
                        await _emailService.SendEmailAsync(emailBloqueado);
                        break;
                }
            }

            _logger.LogInformation(LogMessages.EMPRESASNOTIFICADASOUBLOQUEADAS, 
                                   incompletas.Count());
        }
    }
}
