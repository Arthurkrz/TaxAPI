using RegistroNF.Core.Contracts.Service;
using RegistroNF.Core.Entities;
using TaxAPI.Core.Entities;

namespace TaxAPI.Services
{
    public class NotaFiscalService : INotaFiscalService
    {
        public NotaFiscalService()
        {

        }

        public void EmitirNota(NotaFiscal NF)
        {

        }

        public void CadastroEmpresa(Empresa empresa)
        {

        }

        public void CadastrarNota(NotaFiscal NF)
        {
            throw new NotImplementedException();
        }

        public bool EhValorTotalCorreto(NotaFiscal NF)
        {
            throw new NotImplementedException();
        }

        public bool EhDataComNumeroValido(NotaFiscal NF)
        {
            throw new NotImplementedException();
        }

        public bool EhExistente(int numero)
        {
            throw new NotImplementedException();
        }

        public NotaFiscal BuscarPorNumero(int numero)
        {
            throw new NotImplementedException();
        }
    }
}
