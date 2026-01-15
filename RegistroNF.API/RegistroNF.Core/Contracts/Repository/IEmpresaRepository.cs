using RegistroNF.Architecture.Repositories;
using RegistroNF.Core.Entities;

namespace RegistroNF.Core.Contracts.Repository
{
    public interface IEmpresaRepository : IBaseRepository<Empresa>
    {
        bool EhExistente(string cnpj);

        Empresa GetByCNPJ(string cnpj);

        IEnumerable<Empresa> GetEmpresaByDateAsync(DateTime data);
    }
}
