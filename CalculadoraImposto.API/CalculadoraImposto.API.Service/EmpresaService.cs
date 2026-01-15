using CalculadoraImposto.API.Core.Contracts.Repository;
using CalculadoraImposto.API.Core.Contracts.Service;
using CalculadoraImposto.API.Core.Entities;

namespace CalculadoraImposto.API.Service
{
    public class EmpresaService : IEmpresaService
    {
        private readonly IEmpresaRepository _empresaRepository;

        public EmpresaService(IEmpresaRepository empresaRepository)
        {
            _empresaRepository = empresaRepository;
        }

        public async Task GetOrCreate(Empresa empresa)
        {
            var empresaDb = await _empresaRepository.GetAsync(empresa.CNPJ);

            if (empresaDb is null)
                await _empresaRepository.CreateAsync(empresa);

            else empresa.ID = empresaDb.ID;

            await RegistraNotas(empresa);
        }

        private async Task RegistraNotas(Empresa empresa)
        {
            var notas = new List<NotaFiscal>();

            foreach (var nota in empresa.NotasFiscais)
            {
                nota.EmpresaId = empresa.ID;
                notas.Add(nota);
            }

            await _empresaRepository.RegistraNFsAsync(notas);
        }
    }
}
