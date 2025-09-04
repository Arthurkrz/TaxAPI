namespace TaxAPI.Core.DTOs
{
    public class DTOEmitente
    {
        public string CNPJ { get; set; } = default!;
        public string RazaoSocial { get; set; } = default!;
        public string NomeFantasia { get; set; } = default!;
        public DTOEndereco Endereco { get; set; } = default!;
    }
}
