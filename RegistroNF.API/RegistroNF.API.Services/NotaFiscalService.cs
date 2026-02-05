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
                var errors = string.Join(", ", validationResult.Errors
                    .Select(e => e.ErrorMessage));

                _logger.LogError(errors);
                throw new BusinessRuleException(errors);
            }            

            if (!await EhDataComNumeroValidoAsync(NF)) return;

            var empresa = await _empresaService.CadastroEmpresaAsync(NF.Empresa);

            NF.Empresa = empresa;
            NF.EmpresaId = empresa.Id;

            _notaFiscalRepository.Create(NF);

            _logger.LogInformation(LogMessages.NFCRIADA, NF.Empresa.CNPJ);
        }

        private async Task<bool> EhDataComNumeroValidoAsync(NotaFiscal newNF)
        {
            var notasFiscais = await _notaFiscalRepository.GetSerieNFAsync(newNF.Empresa.CNPJ, newNF.Serie);

            if (!notasFiscais.Any()) return true;

            if (notasFiscais.Any(x => x.Numero == newNF.Numero))
            {
                var error = LogMessages.NFNUMEROEXISTENTE.Replace(
                    "{numero}", newNF.Numero.ToString());

                _logger.LogError(error);
                throw new BusinessRuleException(error);
            }

            if (notasFiscais.FirstOrDefault(
                x => x.Numero < newNF.Numero && 
                x.DataEmissao > newNF.DataEmissao) is not null)
            {
                var error = LogMessages.NFRECENTENUMEROMENOR.Replace(
                    "{serie}", newNF.Serie.ToString());

                _logger.LogError(error);
                throw new BusinessRuleException(error);
            }

            if (notasFiscais.FirstOrDefault(
                x => x.Numero > newNF.Numero && 
                x.DataEmissao < newNF.DataEmissao) is not null)
            {
                var error = LogMessages.NFANTIGANUMEROMAIOR.Replace(
                    "{serie}", newNF.Serie.ToString());

                _logger.LogError(error);
                throw new BusinessRuleException(error);
            }

            return true;
        }
    }
}