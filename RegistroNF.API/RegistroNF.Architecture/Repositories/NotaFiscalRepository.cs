using Microsoft.EntityFrameworkCore;
using RegistroNF.Core.Contracts.Repository;
using RegistroNF.Core.Entities;

namespace RegistroNF.Architecture.Repositories
{
    public class NotaFiscalRepository : BaseRepository<NotaFiscal>, INotaFiscalRepository
    {
        public NotaFiscalRepository(Context context) : base(context) { }

        public async Task<IList<NotaFiscal>> GetSerieNFAsync(string cnpj, int serie) =>
            await this.Get().AsNoTracking()
                      .Include(x => x.Empresa)
                      .ThenInclude(e => e.Endereco)
                      .Where(x => x.Serie.Equals(serie) && 
                             x.Empresa.CNPJ.Equals(cnpj)).ToListAsync();
    }
}
