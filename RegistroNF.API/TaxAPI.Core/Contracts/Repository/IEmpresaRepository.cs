using RegistroNF.Core.Entities;

namespace RegistroNF.Core.Contracts.Repository
{
    public interface IEmpresaRepository
    {
        void Cadastrar(Empresa empresa);

        Empresa GetByCNPJ(string cnpj);
    }
}
