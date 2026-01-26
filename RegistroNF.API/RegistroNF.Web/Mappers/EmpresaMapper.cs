using RegistroNF.Core.Entities;
using RegistroNF.Web.DTOs;

namespace RegistroNF.Web.Mappers
{
    public static class EmpresaMapper
    {
        public static EmpresaDTO ToDTO(this Empresa empresa) =>
            new EmpresaDTO()
            {
                RazaoSocial = empresa.RazaoSocial,
                CNPJ = empresa.CNPJ,
                NomeResponsavel = empresa.NomeResponsavel,
                EmailResponsavel = empresa.EmailResponsavel,
                NotasFiscais = empresa.NotasFiscais.Select(x => new NotaFiscalExportDTO()
                {
                    Numero = x.Numero,
                    Serie = x.Serie,
                    DataEmissao = x.DataEmissao,
                    ValorTotal = x.ValorTotalNota
                }).ToList()
            };
    }
}
