using CalculadoraImposto.API.Core.Common;
using CalculadoraImposto.API.Core.Contracts.Repository;
using CalculadoraImposto.API.Core.Contracts.Service;
using CalculadoraImposto.API.Core.Entities;
using Microsoft.Extensions.Logging;

namespace CalculadoraImposto.API.Service
{
    public class EmpresaService : IEmpresaService
    {
        private readonly IEmpresaRepository _empresaRepository;
        private readonly INotaFiscalRepository _notaFiscalRepository;
        private readonly ILogger<EmpresaService> _logger;

        public EmpresaService(IEmpresaRepository empresaRepository, INotaFiscalRepository notaFiscalRepository, ILogger<EmpresaService> logger)
        {
            _empresaRepository = empresaRepository;
            _notaFiscalRepository = notaFiscalRepository;
            _logger = logger;
        }

        public async Task GetOrCreate(Empresa empresa)
        {
            var empresaDb = await _empresaRepository.GetAsync(empresa.CNPJ);

            if (empresaDb is not null)
            {
                _logger.LogInformation(LogMessages.EMPRESAEXISTENTE, empresa.CNPJ);

                empresa.ID = empresaDb.ID;

                foreach (var nota in empresa.NotasFiscais)
                    nota.EmpresaId = empresaDb.ID;

                await _notaFiscalRepository.RegistraNFsAsync(empresa.NotasFiscais.ToList());

                _logger.LogInformation(LogMessages.NOTASFISCAISREGISTRADAS, 
                    empresa.NotasFiscais.Count, empresa.CNPJ);

                return;
            }

            _logger.LogInformation(LogMessages.EMPRESAINEXISTENTE, empresa.CNPJ);

            await _empresaRepository.CreateAsync(empresa);
        }
    }
}
