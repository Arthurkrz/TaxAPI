namespace RegistroNF.API.Core.Entities
{
    public class Endereco : Entity
    {
        public string? Municipio { get; set; }

        public string? Logradouro { get; set; }

        public int? Numero { get; set; }

        public int? CEP { get; set; }

        public Empresa Empresa { get; set; } = new Empresa();

        public Guid EmpresaId { get; set; } = Guid.Empty;
    }
}
