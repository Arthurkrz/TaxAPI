using CalculadoraImposto.API.Core.Entities;

namespace CalculadoraImposto.API.Core.Contracts.Repository
{
    public interface IEmpresaRepository : IBaseRepository<Empresa>
    {
        Task RegistraNFsAsync(IList<NotaFiscal> notasFiscais);

        Task<Empresa?> GetAsync(string cnpj);
    }
}