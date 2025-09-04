namespace TaxAPI.Core.DTOs
{
    public class DTOTotal
    {
        public double ValorProdutos { get; set; }

        public double ValorIPI { get; set; }

        public double ValorICMS { get; set; }

        public double ValorTotalNota { get; private set; }

        public DTOTotal(double valorProdutos, double valorIPI, double valorICMS)
        {
            ValorProdutos = valorProdutos;
            ValorIPI = valorIPI;
            ValorICMS = valorICMS;
            ValorTotalNota = ValorProdutos + ValorIPI + ValorICMS;
        }
    }
}
