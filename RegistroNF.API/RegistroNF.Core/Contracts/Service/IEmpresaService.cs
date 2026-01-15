using RegistroNF.Core.Entities;

namespace RegistroNF.Core.Contracts.Service
{
    public interface IEmpresaService
    {
        Empresa CadastroEmpresa(Empresa empresa);

        IEnumerable<Empresa> GetEmpresaByDateAsync(int mes, int ano);
    }
}
