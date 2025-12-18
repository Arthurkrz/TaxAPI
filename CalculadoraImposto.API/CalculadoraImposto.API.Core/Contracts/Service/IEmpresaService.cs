using CalculadoraImposto.API.Core.Entities;

namespace CalculadoraImposto.API.Core.Contracts.Service
{
    public interface IEmpresaService
    {
        Task GetOrCreate(Empresa empresa);
    }
}
