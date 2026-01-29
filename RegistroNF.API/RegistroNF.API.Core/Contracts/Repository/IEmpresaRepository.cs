using RegistroNF.API.Core.Entities;

namespace RegistroNF.API.Core.Contracts.Repository
{
    public interface IEmpresaRepository : IBaseRepository<Empresa>
    {
        Task<bool> EhExistenteAsync(string cnpj);

        Task<Empresa> GetByCNPJAsync(string cnpj);

        Task<IEnumerable<Empresa>> GetEmpresaByDateAsync(DateTime data);
    }
}
