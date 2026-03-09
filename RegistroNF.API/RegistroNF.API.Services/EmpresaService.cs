using FluentValidation;
using Microsoft.Extensions.Logging;
using RegistroNF.API.Core.Common;
using RegistroNF.API.Core.Contracts.Repository;
using RegistroNF.API.Core.Contracts.Service;
using RegistroNF.API.Core.Contracts.SMTPService;
using RegistroNF.API.Core.Entities;
using RegistroNF.API.Core.Enum;
using RegistroNF.API.Services.Utilities;

namespace RegistroNF.API.Services
{
    public class EmpresaService : IEmpresaService
    {
        private readonly IValidator<Empresa> _validatorEmpresa;
        private readonly IEmpresaRepository _empresaRepository;
        private readonly IEmailService _emailService;
        private ILogger<EmpresaService> _logger;

        public EmpresaService(IValidator<Empresa> validatorEmpresa, IEmpresaRepository empresaRepository, IEmailService emailService, ILogger<EmpresaService> logger)
        {
            _validatorEmpresa = validatorEmpresa;
            _empresaRepository = empresaRepository;
            _emailService = emailService;
            _logger = logger;
        }

        public async Task<Empresa> CadastroEmpresaAsync(Empresa empresa)
        {
            var validationResult = _validatorEmpresa.Validate(empresa);

            if (!validationResult.IsValid)
            {
                string errors = string.Join(", ", validationResult.Errors
                    .Select(e => e.ErrorMessage));

                _logger.LogError(errors);
                throw new BusinessRuleException(errors);
            }

            if (!await _empresaRepository.EhExistenteAsync(empresa.CNPJ))
            {
                empresa.Status = AvaliarStatus(empresa);

                EmailMessage email = empresa.Status == Status.Parcial ? 
                    EmailBuilder.BuildEmailParcial(empresa) : 
                    EmailBuilder.BuildEmailCompleto(empresa);

                await _emailService.SendEmailAsync(email);

                await _empresaRepository.CreateAsync(empresa);
                _logger.LogInformation(LogMessages.EMPRESACRIADA, empresa.CNPJ);
            }

            else _logger.LogInformation(LogMessages.EMPRESAEXISTENTE, empresa.CNPJ);

            return await _empresaRepository.GetByCNPJAsync(empresa.CNPJ);
        }

        public async Task UpdateStatusCadastroAsync(Empresa empresa)
        {
            var empresaDb = await _empresaRepository.GetByCNPJAsync(empresa.CNPJ);

            if (empresaDb is not null)
            {
                var validationResult = _validatorEmpresa.Validate(empresa);

                if (!validationResult.IsValid)
                {
                    string errors = string.Join(", ", validationResult.Errors
                        .Select(e => e.ErrorMessage));

                    _logger.LogError(errors);
                    throw new BusinessRuleException(errors);
                }

                var status = AvaliarStatus(empresa);

                if (status == Status.Completo)
                {
                    empresaDb.NomeResponsavel = empresa.NomeResponsavel;
                    empresaDb.EmailResponsavel = empresa.EmailResponsavel;
                    empresaDb.RazaoSocial = empresa.RazaoSocial;
                    empresaDb.NomeFantasia = empresa.NomeFantasia;
                    empresaDb.Endereco = empresa.Endereco;
                    empresaDb.Status = status;

                    await _empresaRepository.UpdateAsync(empresaDb);
                    _logger.LogInformation(LogMessages.CADASTROATUALIZADO);

                    var emailCompleto = EmailBuilder.BuildEmailCompleto(empresa);
                    await _emailService.SendEmailAsync(emailCompleto);
                }

                else
                {
                    _logger.LogError(LogMessages.NOVAEMPRESAPARCIAL);
                    throw new BusinessRuleException(LogMessages.NOVAEMPRESAPARCIAL);
                }
            }

            else
            {
                _logger.LogError(LogMessages.EMPRESANAOENCONTRADA);
                throw new BusinessRuleException(LogMessages.EMPRESANAOENCONTRADA);
            }
        }

        public async Task<IEnumerable<Empresa>> GetEmpresasIncompletasAsync() =>
            await _empresaRepository.GetEmpresasIncompletasAsync();

        public async Task<IEnumerable<Empresa>> GetEmpresaByDateAsync(int mes, int ano) =>
            await _empresaRepository.GetEmpresaByDateAsync(new DateTime(ano, mes, 1));

        private static Status AvaliarStatus(Empresa empresa)
        {
            var status = RegisterStatusValidator.ValidateRegisterStatus(empresa);

            if (status == Status.Completo)
                status = empresa.Endereco is null ? Status.Parcial :
                    RegisterStatusValidator.ValidateRegisterStatus(empresa.Endereco);

            return status;
        }
    }
}
