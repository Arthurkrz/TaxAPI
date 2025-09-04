using TaxAPI.Core.Enum;

namespace RegistroNF.Core.Entities
{
    public class Endereco
    {
        public string Municipio { get; set; } = default!;
        public string Logradouro { get; set; } = default!;
        public string Numero { get; set; } = default!;
        public string CEP { get; set; } = default!;
        public UF UF { get; set; }
    }
}
