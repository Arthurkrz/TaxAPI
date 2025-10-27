using FluentValidation;
using RegistroNF.Core.Common;
using RegistroNF.Core.Contracts.Repository;
using RegistroNF.Core.Contracts.Service;
using RegistroNF.Core.Entities;

namespace RegistroNF.Services
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

        public void CadastroEmpresa(Empresa empresa)
        {
            var validationResult = _validatorEmpresa.Validate(empresa);

            if (!validationResult.IsValid)
                throw new BusinessRuleException(string.Join(
                    ", ", validationResult.Errors.Select(e => e.ErrorMessage)));

            if (!_empresaRepository.EhExistente(empresa.CNPJ))
                    _empresaRepository.Create(empresa);
        }
    }
}
