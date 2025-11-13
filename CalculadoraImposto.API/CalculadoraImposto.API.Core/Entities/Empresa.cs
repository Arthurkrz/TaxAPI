namespace CalculadoraImposto.API.Core.Entities
{
    public class Empresa : Entity
    {   
        public string CNPJ { get; set; } = default!;

        public string RazaoSocial { get; set; } = default!;

        public string NomeResponsavel { get; set; } = default!;

        public string EmailResponsavel { get; set; } = default!;

        public IEnumerable<NotaFiscal> NotasFiscais { get; set; } = new List<NotaFiscal>();

        public IEnumerable<Imposto> Impostos { get; set; } = new List<Imposto>();
    }
}
