using CalculadoraImposto.API.Core.Entities;

namespace CalculadoraImposto.API.Tests.Builders
{
    public class EmpresaBuilder
    {
        private readonly Empresa _empresa = new();

        public static EmpresaBuilder Create() => new EmpresaBuilder();

        public EmpresaBuilder WithNotaFiscal(double valorTotal, int quantidadeNotas = 1)
        {
            var notasFiscais = new NotaFiscal[quantidadeNotas];

            for (int i = 0; i < quantidadeNotas; i++)
            {
                notasFiscais[i] = NotaFiscalBuilder.Create()
                    .WithValorTotal(valorTotal)
                    .Build();
            }

            _empresa.NotasFiscais = notasFiscais;
            return this;
        }

        public EmpresaBuilder WithCNPJ(string cnpj)
        {
            _empresa.CNPJ = cnpj;
            return this;
        }

        public Empresa Build() => _empresa;
    }
}
