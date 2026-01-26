namespace CalculadoraImposto.API.Core.Entities
{
    public class NotaFiscal : Entity
    {
        public int Numero { get; set; }

        public int Serie { get; set; }

        public DateTime DataEmissao { get; set; }

        public double ValorTotal { get; set; }

        public Empresa Empresa { get; set; } = default!;

        public Guid EmpresaId { get; set; }
    }
}
