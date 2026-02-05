using FluentValidation;
using Microsoft.Extensions.Logging;
using RegistroNF.API.Core.Common;
using RegistroNF.API.Core.Contracts.Repository;
using RegistroNF.API.Core.Contracts.Service;
using RegistroNF.API.Core.Entities;

namespace RegistroNF.API.Services
{
    public class NotaFiscalService : INotaFiscalService
    {
        private readonly IValidator<NotaFiscal> _validatorNotaFiscal;
        private readonly IEmpresaService _empresaService;
        private readonly INotaFiscalRepository _notaFiscalRepository;
        private readonly ILogger<NotaFiscalService> _logger;

        public NotaFiscalService(IValidator<NotaFiscal> validatorNF, 
                                 IEmpresaService empresaService, 
                                 INotaFiscalRepository nfRepository, 
                                 ILogger<NotaFiscalService> logger)
        {
            _validatorNotaFiscal = validatorNF;
            _empresaService = empresaService;
            _notaFiscalRepository = nfRepository;
            _logger = logger;
        }

        public async Task EmitirNotaAsync(NotaFiscal NF)
        {
            var validationResult = _validatorNotaFiscal.Validate(NF);

            if (!validationResult.IsValid)
            {
                var valorNumero = NF.Numero <= 0 ?
                    "não informado" : NF.Numero.ToString();

                var valorSerie = NF.Serie <= 0 ?
                    "não informada" : NF.Serie.ToString();

                _logger.LogError(LogMessages.NFINVALIDA, valorNumero, valorSerie,
                    string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)));

                throw new BusinessRuleException(string.Format(
                    LogMessages.NFINVALIDA, valorNumero, valorSerie, 
                    string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage))));
            }            

            if (!await EhDataComNumeroValidoAsync(NF)) return;

            var empresa = await _empresaService.CadastroEmpresaAsync(NF.Empresa);

            NF.Empresa = empresa;
            NF.EmpresaId = empresa.Id;

            _notaFiscalRepository.Create(NF);
        }

        private async Task<bool> EhDataComNumeroValidoAsync(NotaFiscal newNF)
        {
            var notasFiscais = await _notaFiscalRepository.GetSerieNFAsync(newNF.Empresa.CNPJ, newNF.Serie);

            if (!notasFiscais.Any()) return true;

            if (notasFiscais.Any(x => x.Numero == newNF.Numero))
            {
                _logger.LogError(LogMessages.NFNUMEROEXISTENTE,
                    newNF.Numero, newNF.Serie, newNF.Empresa.CNPJ);

                throw new BusinessRuleException(string.Format(
                    LogMessages.NFNUMEROEXISTENTE,
                    newNF.Numero, newNF.Serie, newNF.Empresa.CNPJ));
            }

            if (notasFiscais.FirstOrDefault(
                x => x.Numero < newNF.Numero && 
                x.DataEmissao > newNF.DataEmissao) is not null)
            {
                _logger.LogError(LogMessages.NFRECENTENUMEROMENOR,
                    newNF.Serie, newNF.Empresa.CNPJ);

                throw new BusinessRuleException(string.Format(
                    LogMessages.NFRECENTENUMEROMENOR,
                    newNF.Serie, newNF.Empresa.CNPJ));
            }

            if (notasFiscais.FirstOrDefault(
                x => x.Numero > newNF.Numero && 
                x.DataEmissao < newNF.DataEmissao) is not null)
            {
                _logger.LogError(LogMessages.NFANTIGANUMEROMAIOR,
                    newNF.Serie, newNF.Empresa.CNPJ);

                throw new BusinessRuleException(string.Format(
                    LogMessages.NFANTIGANUMEROMAIOR,
                    newNF.Serie, newNF.Empresa.CNPJ));
            }

            return true;
        }
    }
}