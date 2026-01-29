using RegistroNF.API.Core.Entities;

namespace RegistroNF.API.Core.Contracts.Service
{
    public interface IEmpresaService
    {
        Task<Empresa> CadastroEmpresaAsync(Empresa empresa);

        Task<IEnumerable<Empresa>> GetEmpresaByDateAsync(int mes, int ano);
    }
}
