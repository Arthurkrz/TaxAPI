using RegistroNF.Core.Enum;

namespace RegistroNF.Core.Entities
{
    public class Endereco : Entity
    {
        public string Municipio { get; set; } = default!;

        public string Logradouro { get; set; } = default!;

        public int Numero { get; set; } = default!;

        public int CEP { get; set; } = default!;

        public UF UF { get; set; }

        public Empresa Empresa { get; set; } = new Empresa();

        public Guid EmpresaId { get; set; }
    }
}
