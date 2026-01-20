using CalculadoraImposto.API.Core.Contracts.Repository;
using CalculadoraImposto.API.Core.Contracts.Service;
using CalculadoraImposto.API.Core.Entities;

namespace CalculadoraImposto.API.Service
{
    public class ImpostoService : IImpostoService
    {
        private readonly IEmpresaService _empresaService;
        private readonly IImpostoRepository _impostoRepository;

        public ImpostoService(IEmpresaService empresaService, IImpostoRepository impostoRepository)
        {
            _empresaService = empresaService;
            _impostoRepository = impostoRepository;
        }

        public async Task ProcessarImposto(IEnumerable<Empresa> empresas)
        {
            foreach (Empresa empresa in empresas)
            {
                await _empresaService.GetOrCreate(empresa);

                Imposto imposto = CalcularImposto(empresa);
                await _impostoRepository.CreateAsync(imposto);
            }
        }

        public Imposto CalcularImposto(Empresa empresa)
        {
            var receita = empresa.NotasFiscais.Sum(nf => nf.ValorTotal);
            var receitaDeduzida = receita * 0.9;
            var lucroPresumido = receitaDeduzida * 0.6;
            var aliquota = CalcularAliquota(lucroPresumido);

            var dataReferencia = empresa.NotasFiscais.First().DataEmissao;

            return new Imposto
            {
                ValorImposto = Math.Floor(lucroPresumido * aliquota),
                Aliquota = aliquota,
                Vencimento = DateTime.Now.AddMonths(1).Date,
                LucroPresumido = lucroPresumido,
                AnoReferencia = dataReferencia.Year,
                MesReferencia = dataReferencia.Month,
                EmpresaId = empresa.ID,
            };
        }

        private static double CalcularAliquota(double lucro) =>
            lucro >= 15000 && lucro < 25000 ? 0.12 :
            lucro >= 25000 && lucro < 50000 ? 0.18 :
            lucro >= 50000 && lucro < 75000 ? 0.23 :
            lucro >= 75000 ? 0.28 : 0.0;
    }
}
