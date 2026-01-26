using CalculadoraImposto.API.Core.Contracts.Repository;
using CalculadoraImposto.API.Core.Contracts.Service;
using CalculadoraImposto.API.Core.Entities;
using CalculadoraImposto.API.Service;
using Moq;

namespace CalculadoraImposto.API.Tests
{
    public class EmpresaServiceTests
    {
        private readonly Mock<IEmpresaRepository> _empresaRepositoryMock = new();
        private readonly Mock<INotaFiscalRepository> _nfRepositoryMock = new();
        private readonly IEmpresaService _sut;

        public EmpresaServiceTests()
        {
            _sut = new EmpresaService(_empresaRepositoryMock.Object, _nfRepositoryMock.Object);
        }

        [Fact]
        public async Task GetOrCreate_DeveInvocarMetodoRepositorioCreateAsync_QuandoEmpresaNaoExiste()
        {
            // Arrange
            var empresa = new Empresa()
            {
                ID = Guid.NewGuid(),
                CNPJ = "12345678000190",
                RazaoSocial = "Razao Social",
                NomeResponsavel = "Nome Responsavel",
                EmailResponsavel = "Email Responsavel",
                NotasFiscais = [],
                Impostos = []
            };

            _empresaRepositoryMock.Setup(x => x.GetAsync(
                It.IsAny<string>())).ReturnsAsync((Empresa?)null);

            // Act
            await _sut.GetOrCreate(empresa);

            // Assert
            _empresaRepositoryMock.Verify(x => x.CreateAsync(
                It.IsAny<Empresa>()), Times.Once);
        }

        [Fact]
        public async Task GetOrCreate_DeveInvocarMetodoRepositorioRegistraNFsAsync_QuandoEmpresaExiste()
        {
            // Arrange
            var empresa = new Empresa()
            {
                ID = Guid.NewGuid(),
                CNPJ = "12345678000190",
                RazaoSocial = "Razao Social",
                NomeResponsavel = "Nome Responsavel",
                EmailResponsavel = "Email Responsavel",
                NotasFiscais = [],
                Impostos = []
            };

            var nf = new NotaFiscal()
            {
                Numero = 1,
                Serie = 1,
                DataEmissao = DateTime.Now,
                ValorTotal = 1000.0,
                Empresa = empresa,
                EmpresaId = empresa.ID
            };

            empresa.NotasFiscais.Add(nf);

            _empresaRepositoryMock.Setup(x => x.GetAsync(
                It.IsAny<string>())).ReturnsAsync(empresa);

            // Act
            await _sut.GetOrCreate(empresa);

            // Assert
            _nfRepositoryMock.Verify(x => x.RegistraNFsAsync(
                It.IsAny<List<NotaFiscal>>()), Times.Once);
        }
    }
}
