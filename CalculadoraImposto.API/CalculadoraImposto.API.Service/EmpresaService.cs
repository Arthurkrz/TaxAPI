using CalculadoraImposto.API.Core.Contracts.Repository;
using CalculadoraImposto.API.Core.Contracts.Service;
using CalculadoraImposto.API.Core.Entities;

namespace CalculadoraImposto.API.Service
{
    public class EmpresaService : IEmpresaService
    {
        private readonly IEmpresaRepository _empresaRepository;
        private readonly INotaFiscalRepository _notaFiscalRepository;

        public EmpresaService(IEmpresaRepository empresaRepository, INotaFiscalRepository notaFiscalRepository)
        {
            _empresaRepository = empresaRepository;
            _notaFiscalRepository = notaFiscalRepository;
        }

        public async Task GetOrCreate(Empresa empresa)
        {
            var empresaDb = await _empresaRepository.GetAsync(empresa.CNPJ);

            if (empresaDb is not null)
            {
                empresa.ID = empresaDb.ID;

                foreach (var nota in empresa.NotasFiscais)
                    nota.EmpresaId = empresaDb.ID;

                await _notaFiscalRepository.RegistraNFsAsync(empresa.NotasFiscais.ToList());

                return;
            }

            await _empresaRepository.CreateAsync(empresa);
        }
    }
}
