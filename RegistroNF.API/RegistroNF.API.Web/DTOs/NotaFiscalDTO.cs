namespace RegistroNF.API.Web.DTOs
{
    public class NotaFiscalDTO
    {
        public int Numero { get; set; }

        public int Serie { get; set; }
        
        public DateTime DataEmissao { get; set; }
        
        public double ValorBrutoProdutos { get; set; }
        
        public double ValorICMS { get; set; }
        
        public double ValorTotalNota { get; set; }

        public string CNPJ { get; set; } = default!;
        
        public string NomeResponsavel { get; set; } = default!;
        
        public string EmailResponsavel { get; set; } = default!;

        public string? RazaoSocial { get; set; }
        
        public string? NomeFantasia { get; set; }
        
        public string? Municipio { get; set; }
        
        public string? Logradouro { get; set; }
        
        public int? NumeroEndereco { get; set; }
        
        public int? CEP { get; set; }
        
        public string? UF { get; set; }
    }
}