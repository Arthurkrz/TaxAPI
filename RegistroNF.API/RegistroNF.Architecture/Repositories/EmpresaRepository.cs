using Microsoft.EntityFrameworkCore;
using RegistroNF.Core.Contracts.Repository;
using RegistroNF.Core.Entities;

namespace RegistroNF.Architecture.Repositories
{
    public class EmpresaRepository : BaseRepository<Empresa>, IEmpresaRepository
    {
        public EmpresaRepository(Context context) : base(context) { }

        public bool EhExistente(string cnpj) =>
            this.Get().Any(e => e.CNPJ == cnpj);

        public Empresa GetByCNPJ(string cnpj) =>
            this.Get().FirstOrDefault(e => e.CNPJ == cnpj)!;

        public IEnumerable<Empresa> GetEmpresaByDateAsync(DateTime data) =>
            Get().Where(n => n.NotasFiscais
            .Any(n => n.DataEmissao.Year == data.Year
                   && n.DataEmissao.Month == data.Month))

            .Include(x => x.NotasFiscais
            .Where(n => n.DataEmissao.Year == data.Year
                     && n.DataEmissao.Month == data.Month)).ToList();
    }
}
