namespace TaxAPI.Core.DTOs
{
    public class DTOIde
    {
        public int Numero { get; set; }
        public int Serie { get; set; }
        public DateTime DataEmissao { get; set; }
        public string NaturezaOperacao { get; set; } = default!;

        public DTOIde(int numero, int serie, DateTime dataEmissao, string naturezaOperacao)
        {
            Numero = numero;
            Serie = serie;
            DataEmissao = dataEmissao;
            NaturezaOperacao = naturezaOperacao;
        }
    }
}
