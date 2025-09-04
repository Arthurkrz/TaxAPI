using RegistroNF.Core.Entities;
using TaxAPI.Core.DTOs;

namespace TaxAPI.Core.Entities
{
    public class NotaFiscal
    {
        public DTOIde Ide { get; set; } = default!;
        public Empresa Empresa { get; set; } = default!;
        public DTOTotal Total { get; set; } = default!;

        public NotaFiscal(DTOIde ide, Empresa empresa, DTOTotal total)
        {
            Ide = ide;
            Empresa = empresa;
            Total = total;
        }
    }
}
