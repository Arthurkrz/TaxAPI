using Microsoft.EntityFrameworkCore;
using RegistroNF.API.Core.Contracts.Repository;
using RegistroNF.API.Core.Entities;
using RegistroNF.API.Core.Enum;

namespace RegistroNF.API.Infrastructure.Repositories
{
    public class EmpresaRepository : BaseRepository<Empresa>, IEmpresaRepository
    {
        public EmpresaRepository(Context context) : base(context) { }

        public async Task<bool> EhExistenteAsync(string cnpj) =>
            await this.Get().AnyAsync(e => e.CNPJ == cnpj);

        public async Task<Empresa> GetByCNPJAsync(string cnpj) =>
            (await this.Get().Include(emp => emp.Endereco).FirstOrDefaultAsync(e => e.CNPJ == cnpj))!;
         
        public async Task<IEnumerable<Empresa>> GetEmpresaByDateAsync(DateTime data) =>
            await Get().Where(n => n.NotasFiscais
                  .Any(n => n.DataEmissao.Year == data.Year
                         && n.DataEmissao.Month == data.Month))

                  .Include(x => x.NotasFiscais
                  .Where(n => n.DataEmissao.Year == data.Year
                           && n.DataEmissao.Month == data.Month)).ToListAsync();

        public async Task<IEnumerable<Empresa>> GetEmpresasIncompletasAsync() =>
            await Get().Where(e => e.Status == Status.Parcial || e.Status == Status.Notificado).ToListAsync();
    }
}
