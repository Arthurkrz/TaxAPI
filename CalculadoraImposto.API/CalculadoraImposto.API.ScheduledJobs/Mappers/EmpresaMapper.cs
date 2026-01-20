using CalculadoraImposto.API.Core.Entities;
using CalculadoraImposto.API.ScheduledJobs.DTOs;

namespace CalculadoraImposto.API.ScheduledJobs.Mappers
{
    public static class EmpresaMapper
    {
        public static Empresa ToEntity(this EmpresaDTO empresaDTO) =>
            new Empresa()
            {
                CNPJ = empresaDTO.CNPJ,
                RazaoSocial = empresaDTO.RazaoSocial,
                NomeResponsavel = empresaDTO.NomeResponsavel,
                EmailResponsavel = empresaDTO.EmailResponsavel,
                NotasFiscais = empresaDTO.NotasFiscais
                    .Select(nfDTO => nfDTO.ToEntity())
                    .ToList()
            };

    }
}
