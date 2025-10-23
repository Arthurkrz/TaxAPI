using RegistroNF.Core.Entities;

namespace TaxAPI.Core.Entities
{
    public class NotaFiscal : Entity
    {
        public int Numero { get; set; }

        public int Serie { get; set; }

        public DateTime DataEmissao { get; set; }

        public double ValorBrutoProdutos { get; set; }

        public double ValorICMS { get; set; }

        public double ValorTotalNota { get; set; }

        public Empresa Empresa { get; set; } = default!;

        public Guid EmpresaId { get; set; }
    }
}
