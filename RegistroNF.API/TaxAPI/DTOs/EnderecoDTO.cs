namespace TaxAPI.Core.DTOs
{
    public class EnderecoDTO
    {
        public string? Municipio { get; set; }

        public string? Logradouro { get; set; }

        public string? Numero { get; set; }
        
        public string? CEP { get; set; }
        
        public string? UF { get; set; }
    }
}
