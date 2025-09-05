using RegistroNF.Core.Entities;
using TaxAPI.Core.DTOs;

namespace RegistroNF.Core.Contracts.Factory
{
    public interface IEnderecoFactory
    {
        Endereco Create(EnderecoDTO endereco);
    }
}
