using TaxAPI.Core.Enum;

namespace RegistroNF.Core.Entities
{
    public class Endereco : Entity
    {
        public string Municipio { get; set; } = default!;

        public string Logradouro { get; set; } = default!;

        public int Numero { get; set; } = default!;

        public int CEP { get; set; } = default!;

        public UF UF { get; set; }
    }
}
