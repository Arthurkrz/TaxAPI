namespace RegistroNF.API.Web.DTOs
{
    public record EmpresaDTO
    {
        public string RazaoSocial { get; set; } = string.Empty;

        public string CNPJ { get; set; } = string.Empty;

        public string NomeResponsavel { get; set; } = string.Empty;

        public string EmailResponsavel { get; set; } = string.Empty;

        public List<NotaFiscalExportDTO> NotasFiscais { get; set; } = new();
    }
}
