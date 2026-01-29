using FluentValidation;
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

        public EmpresaService(IValidator<Empresa> validatorEmpresa, IEmpresaRepository empresaRepository)
        {
            _validatorEmpresa = validatorEmpresa;
            _empresaRepository = empresaRepository;
        }

        public async Task<Empresa> CadastroEmpresaAsync(Empresa empresa)
        {
            var validationResult = _validatorEmpresa.Validate(empresa);

            if (!validationResult.IsValid)
                throw new BusinessRuleException(string.Join(
                    ", ", validationResult.Errors.Select(e => e.ErrorMessage)));

            if (!await _empresaRepository.EhExistenteAsync(empresa.CNPJ))
                    _empresaRepository.Create(empresa);

            return await _empresaRepository.GetByCNPJAsync(empresa.CNPJ);
        }

        public async Task<IEnumerable<Empresa>> GetEmpresaByDateAsync(int mes, int ano) =>
            await _empresaRepository.GetEmpresaByDateAsync(new DateTime(ano, mes, 1));
    }
}
