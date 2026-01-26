using CalculadoraImposto.API.Core.Entities;
using CalculadoraImposto.API.ScheduledJobs.DTOs;

namespace CalculadoraImposto.API.ScheduledJobs.Mappers
{
    public static class NotaFiscalMapper
    {
        public static NotaFiscal ToEntity(this NotaFiscalDTO nfDTO) =>
            new NotaFiscal()
            {
                Numero = nfDTO.Numero,
                Serie = nfDTO.Serie,
                DataEmissao = nfDTO.DataEmissao,
                ValorTotal = nfDTO.ValorTotal
            };
    }
}
