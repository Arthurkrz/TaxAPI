using FluentValidation;
using Microsoft.Extensions.Logging;
using RegistroNF.API.Core.Common;
using RegistroNF.API.Core.Contracts.Repository;
using RegistroNF.API.Core.Contracts.Service;
using RegistroNF.API.Core.Entities;

namespace RegistroNF.API.Services
{
    public class EmpresaService : IEmpresaService
    {
        private readonly IValidator<Empresa> _validatorEmpresa;
        private readonly IEmpresaRepository _empresaRepository;
        private ILogger<EmpresaService> _logger;

        public EmpresaService(IValidator<Empresa> validatorEmpresa, IEmpresaRepository empresaRepository, ILogger<EmpresaService> logger)
        {
            _validatorEmpresa = validatorEmpresa;
            _empresaRepository = empresaRepository;
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
                _empresaRepository.Create(empresa);
                _logger.LogInformation(LogMessages.EMPRESACRIADA, empresa.CNPJ);
            }

            else _logger.LogInformation(LogMessages.EMPRESAEXISTENTE, empresa.CNPJ);

            return await _empresaRepository.GetByCNPJAsync(empresa.CNPJ);
        }

        public async Task<IEnumerable<Empresa>> GetEmpresaByDateAsync(int mes, int ano) =>
            await _empresaRepository.GetEmpresaByDateAsync(new DateTime(ano, mes, 1));
    }
}
