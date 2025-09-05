using RegistroNF.Core.DTOs;
using RegistroNF.Core.Entities;

namespace RegistroNF.Core.Contracts.Factory
{
    public interface IContatoFactory
    {
        Contato Create(ContatoDTO contato);
    }
}
