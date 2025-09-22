namespace RegistroNF.Core.Entities
{
    public class Empresa : Entity
    {
        public string CNPJ { get; set; } = default!;

        public string? RazaoSocial { get; set; } = default!;

        public string? NomeFantasia { get; set; } = default!;

        public string NomeResponsavel { get; set; } = default!;

        public string EmailResponsavel { get; set;} = default!;

        public Endereco? Endereco { get; set; } = default!;
    }
}
