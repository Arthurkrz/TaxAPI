using Microsoft.EntityFrameworkCore;
using RegistroNF.Core.Contracts.Repository;
using RegistroNF.Core.Entities;

namespace RegistroNF.Architecture.Repositories
{
    public class EmpresaRepository : BaseRepository<Empresa>, IEmpresaRepository
    {
        public EmpresaRepository(Context context) : base(context) { }

        public async Task<bool> EhExistenteAsync(string cnpj) =>
            await this.Get().AnyAsync(e => e.CNPJ == cnpj);

        public async Task<Empresa> GetByCNPJAsync(string cnpj) =>
            (await this.Get().FirstOrDefaultAsync(e => e.CNPJ == cnpj))!;

        public async Task<IEnumerable<Empresa>> GetEmpresaByDateAsync(DateTime data) =>
            await Get().Where(n => n.NotasFiscais
                  .Any(n => n.DataEmissao.Year == data.Year
                         && n.DataEmissao.Month == data.Month))

                  .Include(x => x.NotasFiscais
                  .Where(n => n.DataEmissao.Year == data.Year
                           && n.DataEmissao.Month == data.Month)).ToListAsync();
    }
}
