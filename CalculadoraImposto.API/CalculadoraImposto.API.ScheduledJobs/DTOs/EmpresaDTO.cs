namespace CalculadoraImposto.API.ScheduledJobs.DTOs
{
    public record EmpresaDTO
    {
        public string CNPJ { get; set; } = string.Empty;

        public string RazaoSocial { get; set; } = string.Empty;

        public string NomeResponsavel { get; set; } = string.Empty;

        public string EmailResponsavel { get; set; } = string.Empty;

        public IList<NotaFiscalDTO> NotasFiscais { get; set; } = [];
    }
}