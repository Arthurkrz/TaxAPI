using RegistroNF.Core.Entities;

namespace RegistroNF.Core.Contracts.Service
{
    public interface INotaFiscalService
    {
        Task EmitirNotaAsync(NotaFiscal NF);
    }
}
