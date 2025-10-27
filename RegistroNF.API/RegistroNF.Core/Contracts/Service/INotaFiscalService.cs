using RegistroNF.Core.Entities;

namespace RegistroNF.Core.Contracts.Service
{
    public interface INotaFiscalService
    {
        void EmitirNota(NotaFiscal NF);
    }
}
