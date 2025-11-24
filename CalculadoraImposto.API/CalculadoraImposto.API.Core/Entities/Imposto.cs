namespace CalculadoraImposto.API.Core.Entities
{
    public class Imposto : Entity
    {
        public double ValorImposto { get; set; }

        public double Aliquota { get; set; }

        public DateTime Vencimento { get; set; }

        public double LucroPresumido { get; set; }

        public int AnoReferencia { get; set; }

        public int MesReferencia { get; set; } 

        public Empresa Empresa { get; set; } = default!;

        public Guid EmpresaId { get; set; }
    }
}
