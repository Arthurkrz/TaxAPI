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

        public IEnumerable<Empresa> GetEmpresaByDateAsync(DateTime data)
        {
            var test = Get().Include(e => e.NotasFiscais);
            
            var result = test.SelectMany(e => e.NotasFiscais)
                .Where(nf => nf.DataEmissao.Month == data.Month && nf.DataEmissao.Year == data.Year)
                .Select(nf => nf.Empresa)
                .Distinct()
                .ToList();

            return result;
        }
    }
}
