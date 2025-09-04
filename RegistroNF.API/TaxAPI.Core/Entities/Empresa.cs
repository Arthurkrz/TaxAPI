namespace RegistroNF.Core.Entities
{
    public class Empresa
    {
        public string CNPJ { get; set; } = default!;
        public string RazaoSocial { get; set; } = default!;
        public string NomeFantasia { get; set; } = default!;
        public Endereco Endereco { get; set; } = default!;
    }
}
