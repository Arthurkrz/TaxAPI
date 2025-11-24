using CalculadoraImposto.API.Core.Entities;

namespace CalculadoraImposto.API.Core.Contracts.Service
{
    public interface IImpostoService
    {
        Imposto CalcularImposto(Empresa empresa);
    }
}
