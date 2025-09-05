using TaxAPI.Core.Entities;

namespace RegistroNF.Core.Contracts.Service
{
    public interface INotaFiscalService
    {
        void CadastrarNota(NotaFiscal NF);

        bool EhValorTotalCorreto(NotaFiscal NF);

        bool EhDataComNumeroValido(NotaFiscal NF);

        bool EhExistente(int numero);

        NotaFiscal BuscarPorNumero(int numero);
    }
}
