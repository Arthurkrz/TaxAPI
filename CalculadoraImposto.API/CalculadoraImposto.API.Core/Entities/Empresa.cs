namespace CalculadoraImposto.API.Core.Entities
{
    public class Empresa : Entity
    {
        public string CNPJ { get; set; } = default!;

        public string RazaoSocial { get; set; } = default!;

        public string NomeResponsavel { get; set; } = default!;

        public string EmailResponsavel { get; set; } = default!;

        public ICollection<NotaFiscal> NotasFiscais { get; set; } = [];

        public ICollection<Imposto> Impostos { get; set; } = [];
    }
}
