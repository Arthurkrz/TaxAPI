namespace RegistroNF.Web.DTOs
{
    public class NotaFiscalExportDTO
    {
        public int Numero { get; set; }

        public int Serie { get; set; }

        public DateTime DataEmissao { get; set; }

        public double ValorTotal { get; set; }
    }
}
