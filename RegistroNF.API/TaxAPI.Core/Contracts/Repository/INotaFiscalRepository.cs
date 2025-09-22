using TaxAPI.Core.Entities;

namespace RegistroNF.Core.Contracts.Repository
{
    public interface INotaFiscalRepository
    {
        List<NotaFiscal> GetSerieNF(int serie);

        void Add(NotaFiscal NF);
    }
}
