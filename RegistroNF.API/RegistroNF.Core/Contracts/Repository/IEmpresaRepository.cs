using RegistroNF.Architecture.Repositories;
using RegistroNF.Core.Entities;

namespace RegistroNF.Core.Contracts.Repository
{
    public interface IEmpresaRepository : IBaseRepository<Empresa>
    {
        Task<bool> EhExistenteAsync(string cnpj);

        Task<Empresa> GetByCNPJAsync(string cnpj);

        Task<IEnumerable<Empresa>> GetEmpresaByDateAsync(DateTime data);
    }
}
