using CalculadoraImposto.API.Core.Entities;

namespace CalculadoraImposto.API.Core.Contracts.Service
{
    public interface IImpostoService
    {
        Task ProcessarImposto(IEnumerable<Empresa> empresas);

        Imposto CalcularImposto(Empresa empresa);
    }
}
