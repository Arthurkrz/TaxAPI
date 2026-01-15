using RegistroNF.Core.DTOs;
using RegistroNF.Core.Entities;

namespace RegistroNF.Web.Mapper
{
    public static class NotaFiscalMapper
    {
        public static NotaFiscal ToEntity(this NotaFiscalDTO nfDTO) =>
            new NotaFiscal()
            {
                Numero = nfDTO.Numero,
                Serie = nfDTO.Serie,
                DataEmissao = nfDTO.DataEmissao,
                ValorBrutoProdutos = nfDTO.ValorBrutoProdutos,
                ValorICMS = nfDTO.ValorICMS,
                ValorTotalNota = nfDTO.ValorTotalNota,
                Empresa = new Empresa()
                {
                    CNPJ = nfDTO.CNPJ,
                    NomeResponsavel = nfDTO.NomeResponsavel,
                    EmailResponsavel = nfDTO.EmailResponsavel,
                    RazaoSocial = nfDTO.RazaoSocial!,
                    NomeFantasia = nfDTO.NomeFantasia,
                    Endereco = new Endereco()
                    {
                        Municipio = nfDTO.Municipio,
                        Logradouro = nfDTO.Logradouro,
                        Numero = nfDTO.NumeroEndereco,
                        CEP = nfDTO.CEP,
                    }
                }
            };
    }
}
