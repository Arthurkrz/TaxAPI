namespace CalculadoraImposto.API.Core.Entities
{
    public class Imposto : Entity
    {
        public int Codigo { get; set; }

        public double ValorAPagar { get; set; }

        public DateTime Vencimento { get; set; }

        public Guid EmpresaId { get; set; }
    }
}
