using FluentValidation;
using RegistroNF.Core.Common;
using RegistroNF.Core.Contracts.Repository;
using RegistroNF.Core.Contracts.Service;
using RegistroNF.Core.Entities;

namespace TaxAPI.Services
{
    public class NotaFiscalService : INotaFiscalService
    {
        private readonly IValidator<NotaFiscal> _validatorNotaFiscal;
        private readonly IEmpresaService _empresaService;
        private readonly INotaFiscalRepository _notaFiscalRepository;
        private readonly IEmpresaRepository _empresaRepository;

        public NotaFiscalService(IValidator<NotaFiscal> validatorNF, 
                                 IEmpresaService empresaService, 
                                 INotaFiscalRepository nfRepository, 
                                 IEmpresaRepository empresaRepository)
        {
            _validatorNotaFiscal = validatorNF;
            _empresaService = empresaService;
            _notaFiscalRepository = nfRepository;
            _empresaRepository = empresaRepository;
        }

        public void EmitirNota(NotaFiscal NF)
        {
            var validationResult = _validatorNotaFiscal.Validate(NF);

            if (!validationResult.IsValid)
                throw new BusinessRuleException(string.Join(
                    ", ", validationResult.Errors.Select(e => e.ErrorMessage)));

            if (!EhDataComNumeroValido(NF))
                throw new BusinessRuleException(ErrorMessages.NFNUMERODATAINVALIDO);

            _empresaService.CadastroEmpresa(NF.Empresa);
            _notaFiscalRepository.Create(NF);
        }

        private bool EhDataComNumeroValido(NotaFiscal newNF)
        {
            var notasFiscais = _notaFiscalRepository.GetSerieNF(newNF.Serie);

            if (!notasFiscais.Any()) return true;

            if (notasFiscais.Any(x => x.Numero == newNF.Numero))
                    throw new BusinessRuleException(ErrorMessages.NFNUMEROEXISTENTE);

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