using CalculadoraImposto.API.Core.Entities;

namespace CalculadoraImposto.API.Tests.Builders
{
    public class NotaFiscalBuilder
    {
        private readonly NotaFiscal _notaFiscal = new() 
            { DataEmissao = DateTime.Now.AddMonths(-1).Date};

        public static NotaFiscalBuilder Create() => new NotaFiscalBuilder();

        public NotaFiscalBuilder WithValorTotal(double valorTotal)
        {
            _notaFiscal.ValorTotal = valorTotal;
            return this;
        }

        public NotaFiscal Build() => _notaFiscal;
    }
}
