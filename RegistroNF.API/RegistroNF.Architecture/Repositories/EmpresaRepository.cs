using RegistroNF.Core.Contracts.Repository;
using RegistroNF.Core.Entities;

namespace RegistroNF.Architecture.Repositories
{
    public class EmpresaRepository : IEmpresaRepository
    {
        public void Cadastrar(Empresa empresa)
        {
            throw new NotImplementedException();
        }

        public bool EhExistente(string cnpj)
        {
            throw new NotImplementedException();
        }
    }
}
