using RegistroNF.Core.Contracts.Repository;
using RegistroNF.Core.Entities;

namespace RegistroNF.Architecture.Repositories
{
    public class EmpresaRepository : BaseRepository<Empresa>, IEmpresaRepository
    {
        public EmpresaRepository(Context context) : base(context) { }

        public bool EhExistente(string cnpj) =>
            this.Get().Any(e => e.CNPJ == cnpj);
    }
}
