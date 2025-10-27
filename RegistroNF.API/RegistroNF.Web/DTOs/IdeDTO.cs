namespace TaxAPI.Core.DTOs
{
    public class IdeDTO
    {
        public int? Numero { get; set; }

        public int? Serie { get; set; }
        
        public DateTime? DataEmissao { get; set; }
        
        public string? NaturezaOperacao { get; set; }
    }
}
