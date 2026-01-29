using RegistroNF.API.Core.Entities;

namespace RegistroNF.API.Core.Contracts.Repository
{
    public interface INotaFiscalRepository : IBaseRepository<NotaFiscal>
    {
        Task<IList<NotaFiscal>> GetSerieNFAsync(string cnpj, int serie);
    }
}
