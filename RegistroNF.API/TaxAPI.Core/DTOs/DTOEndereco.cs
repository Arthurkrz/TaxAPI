namespace TaxAPI.Core.DTOs
{
    public class DTOEndereco
    {
        public string Municipio { get; set; } = default!;
        public string Logradouro { get; set; } = default!;
        public string Numero { get; set; } = default!;
        public string CEP { get; set; } = default!;
        public string UF { get; set; } = default!;
    }
}
