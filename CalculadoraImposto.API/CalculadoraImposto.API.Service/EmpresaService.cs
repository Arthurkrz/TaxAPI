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

            if (empresaDb is null)
            {
                foreach (var nota in empresa.NotasFiscais)
                    nota.NotaFiscalEmpresaId = empresa.ID;

                empresaDb = await _empresaRepository.CreateAsync(empresa);
            }

            empresa.ID = empresaDb.ID;
            await RegistraNotasAsync(empresa);
        }

        private async Task RegistraNotasAsync(Empresa empresa)
        {
            var notas = new List<NotaFiscal>();

            foreach (var nota in empresa.NotasFiscais)
            {
                nota.NotaFiscalEmpresaId = empresa.ID;
                notas.Add(nota);
            }

            await _notaFiscalRepository.RegistraNFsAsync(notas);
        }
    }
}
