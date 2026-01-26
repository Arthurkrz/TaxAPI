using CalculadoraImposto.API.Core.Entities;

namespace CalculadoraImposto.API.Core.Contracts.Repository
{
    public interface INotaFiscalRepository : IBaseRepository<NotaFiscal>
    {
        Task RegistraNFsAsync(IList<NotaFiscal> notasFiscais);
    }
}
