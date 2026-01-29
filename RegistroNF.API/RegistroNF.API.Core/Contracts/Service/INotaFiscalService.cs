using RegistroNF.API.Core.Entities;

namespace RegistroNF.API.Core.Contracts.Service
{
    public interface INotaFiscalService
    {
        Task EmitirNotaAsync(NotaFiscal NF);
    }
}
