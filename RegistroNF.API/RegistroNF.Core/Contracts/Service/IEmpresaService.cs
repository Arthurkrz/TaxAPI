using RegistroNF.Core.Entities;

namespace RegistroNF.Core.Contracts.Service
{
    public interface IEmpresaService
    {
        Task<Empresa> CadastroEmpresaAsync(Empresa empresa);

        Task<IEnumerable<Empresa>> GetEmpresaByDateAsync(int mes, int ano);
    }
}
