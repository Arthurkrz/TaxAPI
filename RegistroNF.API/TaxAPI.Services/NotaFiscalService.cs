using FluentValidation;
using RegistroNF.Core.Common;
using RegistroNF.Core.Contracts.Repository;
using RegistroNF.Core.Contracts.Service;
using RegistroNF.Core.Enum;
using TaxAPI.Core.Entities;

namespace TaxAPI.Services
{
    public class NotaFiscalService : INotaFiscalService
    {
        private readonly IValidator<NotaFiscal> _validatorNotaFiscal;
        private readonly INotaFiscalRepository _notaFiscalRepository;

        private readonly IEmpresaService _empresaService;

        public NotaFiscalService(IValidator<NotaFiscal> validatorNF, 
                                 IEmpresaService empresaService, 
                                 INotaFiscalRepository nfRepository)
        {
            _validatorNotaFiscal = validatorNF;
            _empresaService = empresaService;
            _notaFiscalRepository = nfRepository;
        }

        public void EmitirNota(NotaFiscal NF)
        {
            var validationResult = _validatorNotaFiscal.Validate(NF);

            if (!validationResult.IsValid)
                throw new CustomException(string.Join(
                    ", ", validationResult.Errors.Select(e => e.ErrorMessage)),
                    ErrorType.BussinessRuleViolation);

            if (!EhDataComNumeroValido(NF))
                throw new CustomException(ErrorMessages.NFNUMERODATAINVALIDO,
                                          ErrorType.BussinessRuleViolation);

            _empresaService.CadastroEmpresa(NF.Empresa);
            _notaFiscalRepository.Add(NF);
        }

        private bool EhDataComNumeroValido(NotaFiscal newNF)
        {
            var notasFiscais = _notaFiscalRepository.GetSerieNF(newNF.Serie);

            if (notasFiscais is null || !notasFiscais.Any()) return true;

            if (notasFiscais.Any(nf => nf.Numero == newNF.Numero))
                return false;

            if (notasFiscais.FirstOrDefault(
                x => x.Numero < newNF.Numero && 
                x.DataEmissao > newNF.DataEmissao) is not null) 
                return false;

            if (notasFiscais.FirstOrDefault(
                x => x.Numero > newNF.Numero && 
                x.DataEmissao < newNF.DataEmissao) is not null) 
                return false;

            return true;
        }
    }
}