using CalculadoraImposto.API.Core.Entities;

namespace CalculadoraImposto.API.Core.Contracts.Repository
{
    public interface IImpostoRepository
    {
        Task CreateAsync(Imposto imposto);
    }
}
