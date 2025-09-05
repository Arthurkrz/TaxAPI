using RegistroNF.Core.Entities;
using TaxAPI.Core.DTOs;

namespace RegistroNF.Core.Contracts.Factory
{
    public interface IEmpresaFactory
    {
        Empresa Create(EmitenteDTO emitente, Endereco endereco, Contato contato);
    }
}
