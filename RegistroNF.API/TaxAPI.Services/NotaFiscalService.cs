using FluentValidation;
using RegistroNF.Core.Contracts.Repository;
using RegistroNF.Core.Contracts.Service;
using RegistroNF.Core.Entities;
using TaxAPI.Core.Entities;

namespace TaxAPI.Services
{
    public class NotaFiscalService : INotaFiscalService
    {
        private readonly IValidator<NotaFiscal> _validatorNotaFiscal;
        private readonly INotaFiscalRepository _notaFiscalRepository;

        private readonly IValidator<Empresa> _validatorEmpresa;
        private readonly IEmpresaService _empresaService;

        public NotaFiscalService(IValidator<NotaFiscal> validatorNF, IValidator<Empresa> validatorEmpresa,
                                 IEmpresaService empresaService, INotaFiscalRepository nfRepository)
        {
            _validatorNotaFiscal = validatorNF;
            _validatorEmpresa = validatorEmpresa;
            _empresaService = empresaService;
            _notaFiscalRepository = nfRepository;
        }

        public void EmitirNota(NotaFiscal NF)
        {
            var validationResult = _validatorNotaFiscal.Validate(NF);

            if (!validationResult.IsValid)
                throw new ArgumentException(string.Join(
                    ", ", validationResult.Errors.Select(e => e.ErrorMessage)));

            if (!EhDataComNumeroValido(NF))
                throw new ArgumentException("Já existe uma nota fiscal mais " +
                                            "recente cujo número é superior.");

            _empresaService.CadastroEmpresa(NF.Empresa);
            _notaFiscalRepository.Add(NF);
        }

        private bool EhDataComNumeroValido(NotaFiscal NF)
        {
            var notasFiscais = _notaFiscalRepository.GetSerieNF(NF.Serie);

            if (notasFiscais.Count == 0) return true;

            if (notasFiscais.Any(nf => nf.Numero == NF.Numero))
                return false;

            else if (notasFiscais.First().DataEmissao < NF.DataEmissao && 
                notasFiscais.First().Numero < NF.Numero)
                return true;

            else if (notasFiscais.Last().DataEmissao > NF.DataEmissao &&
                notasFiscais.Last().Numero > NF.Numero)
                return false;

            for (int i = 0; i < notasFiscais.Count - 1; i++)
            {
                if (notasFiscais[i].DataEmissao < NF.DataEmissao &&
                    notasFiscais[i + 1].DataEmissao > NF.DataEmissao)
                {
                    if (notasFiscais[i].Numero < NF.Numero &&
                        notasFiscais[i + 1].Numero > NF.Numero)
                        return true;

                    else return false;
                }
            }

            return false;
        }
    }
}