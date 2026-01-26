using CalculadoraImposto.API.Core.Entities;

namespace CalculadoraImposto.API.Core.Contracts.Service
{
    public interface IImpostoService
    {
        Task ProcessarImpostoAsync(IEnumerable<Empresa> empresas);

        Imposto CalcularImposto(Empresa empresa);
    }
}
