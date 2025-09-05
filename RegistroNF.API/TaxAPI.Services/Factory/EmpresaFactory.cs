using RegistroNF.Core.Contracts.Factory;
using RegistroNF.Core.Entities;
using TaxAPI.Core.DTOs;

namespace RegistroNF.Services.Factory
{
    class EmpresaFactory : IEmpresaFactory
    {
        public Empresa Create(EmitenteDTO emitente, Endereco endereco, Contato contato)
        {
            throw new NotImplementedException();
        }
    }
}
