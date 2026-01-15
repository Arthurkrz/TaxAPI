namespace CalculadoraImposto.API.ScheduledJobs.DTOs
{
    internal record NotaFiscalDTO
    {
        public int Numero { get; set; }

        public int Serie { get; set; }

        public DateTime DataEmissao { get; set; }

        public double ValorTotal { get; set; }
    }
}
