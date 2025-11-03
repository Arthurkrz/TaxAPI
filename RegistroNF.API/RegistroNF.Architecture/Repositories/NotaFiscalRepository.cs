using Microsoft.EntityFrameworkCore;
using RegistroNF.Core.Contracts.Repository;
using RegistroNF.Core.Entities;

namespace RegistroNF.Architecture.Repositories
{
    public class NotaFiscalRepository : BaseRepository<NotaFiscal>, INotaFiscalRepository
    {
        public NotaFiscalRepository(Context context) : base(context) { }

        public IEnumerable<NotaFiscal> GetSerieNF(int serie) =>
            this.Get().AsNoTracking()
                      .Include(x => x.Empresa)
                      .ThenInclude(e => e.Endereco)
                      .Where(x => x.Serie.Equals(serie)).ToList();
    }
}
