using CalculadoraImposto.API.Core.Contracts.Service;
using CalculadoraImposto.API.Service;
using CalculadoraImposto.API.Tests.Builders;

namespace CalculadoraImposto.API.Tests
{
    public class ImpostoServiceTests
    {
        private readonly IImpostoService _sut;

        public ImpostoServiceTests()
        {
            _sut = new ImpostoService();
        }

        [Theory]
        [InlineData(0, 5400, 0.0, 1)]
        [InlineData(1944, 16200, 0.12, 3)]
        [InlineData(5832, 32400, 0.18, 6)]
        [InlineData(12420, 54000, 0.23, 10)]
        [InlineData(21168, 75600, 0.28, 14)]
        public void CalcularImposto_DeveRetornarValorCorretoDeImposto(double valorImpostoEsperado, double lucroPresumidoEsperado, double aliquotaEsperada, int quantidadeNotas)
        {
            // Arrange
            var empresa = EmpresaBuilder.Create().WithNotaFiscal(10000, quantidadeNotas).Build();
            var dataEmissaoNF = empresa.NotasFiscais.First().DataEmissao;

            // Act
            var imposto = _sut.CalcularImposto(empresa);

            // Assert
            Assert.Equal(valorImpostoEsperado, imposto.ValorImposto);
            Assert.Equal(lucroPresumidoEsperado, imposto.LucroPresumido);
            Assert.Equal(aliquotaEsperada, imposto.Aliquota);
            Assert.Equal(DateTime.Now.AddMonths(1).Date, imposto.Vencimento);
            Assert.Equal(dataEmissaoNF.Year, imposto.AnoReferencia);
            Assert.Equal(dataEmissaoNF.Month, imposto.MesReferencia);
            Assert.Equal(empresa.ID, imposto.EmpresaId);
        }
    }
}