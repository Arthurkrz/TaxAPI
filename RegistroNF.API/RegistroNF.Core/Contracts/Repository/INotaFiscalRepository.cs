using RegistroNF.Architecture.Repositories;
using RegistroNF.Core.Entities;

namespace RegistroNF.Core.Contracts.Repository
{
    public interface INotaFiscalRepository : IBaseRepository<NotaFiscal>
    {
        Task<IList<NotaFiscal>> GetSerieNFAsync(string cnpj, int serie);
    }
}
