using RegistroNF.Architecture.Repositories;
using RegistroNF.Core.Entities;

namespace RegistroNF.Core.Contracts.Repository
{
    public interface INotaFiscalRepository : IBaseRepository<NotaFiscal>
    {
        IEnumerable<NotaFiscal> GetSerieNF(string cnpj, int serie);
    }
}
