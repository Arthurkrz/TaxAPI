using CalculadoraImposto.API.Core.Entities;

namespace CalculadoraImposto.API.Core.Contracts.Repository
{
    public interface IEmpresaRepository : IBaseRepository<Empresa>
    {
        Task<Empresa?> GetAsync(string cnpj);
    }
}