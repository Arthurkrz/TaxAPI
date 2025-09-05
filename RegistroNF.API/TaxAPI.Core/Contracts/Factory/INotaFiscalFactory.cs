using RegistroNF.Core.Entities;
using TaxAPI.Core.DTOs;
using TaxAPI.Core.Entities;

namespace RegistroNF.Core.Contracts.Factory
{
    public interface INotaFiscalFactory
    {
        NotaFiscal Create(IdeDTO ide, TotalDTO total, Empresa empresa);
    }
}
