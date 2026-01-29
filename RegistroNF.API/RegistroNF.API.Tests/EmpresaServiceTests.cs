using FluentValidation;
using Moq;
using RegistroNF.API.Core.Common;
using RegistroNF.API.Core.Contracts.Repository;
using RegistroNF.API.Core.Entities;
using RegistroNF.API.Core.Validators;
using RegistroNF.API.Services;

namespace RegistroNF.API.Tests
{
    public class EmpresaServiceTests
    {
        private readonly Mock<IValidator<Empresa>> _empresaValidatorMock;
        private readonly Mock<IEmpresaRepository> _empresaRepositoryMock;
        private EmpresaService _sut;

        public EmpresaServiceTests()
        {
            _empresaValidatorMock = new Mock<IValidator<Empresa>>();
            _empresaRepositoryMock = new Mock<IEmpresaRepository>();
            _sut = new EmpresaService(_empresaValidatorMock.Object, _empresaRepositoryMock.Object);
        }

        [Fact]
        public async Task CadastroEmpresa_DeveInvocarMetodoRepositorioCadastroEmpresa_SeEmpresaNaoExisteAsync()
        {
            // Arrange
            var empresa = new Empresa()
            {
                CNPJ = "12345678000199",
                NomeResponsavel = "Nome Responsável",
                EmailResponsavel = "emailresponsavel@gmail.com"
            };

            _empresaValidatorMock.Setup(x => x.Validate(empresa))
                .Returns(new FluentValidation.Results.ValidationResult());

            _empresaRepositoryMock.Setup(x => x.EhExistenteAsync(
                It.IsAny<string>())).ReturnsAsync(false);

            // Act
            await _sut.CadastroEmpresaAsync(empresa);

            // Assert
            _empresaRepositoryMock.Verify(x => x.Create(empresa), Times.Once);
            _empresaRepositoryMock.Verify(x => x.GetByCNPJAsync(empresa.CNPJ), Times.Once);
        }

        [Fact]
        public async Task CadastroEmpresa_NaoDeveInvocarMetodoRepositorioCadastro_SeEmpresaExisteAsync()
        {
            // Arrange
            var empresa = new Empresa()
            {
                CNPJ = "12345678000199",
                NomeResponsavel = "Nome Responsável",
                EmailResponsavel = "emailresponsavel@gmail.com"
            };

            _empresaValidatorMock.Setup(x => x.Validate(empresa))
                .Returns(new FluentValidation.Results.ValidationResult());

            _empresaRepositoryMock.Setup(x => x.EhExistenteAsync(
                It.IsAny<string>())).ReturnsAsync(true);

            // Act
            await _sut.CadastroEmpresaAsync(empresa);

            // Assert
            _empresaRepositoryMock.Verify(x => x.Create(empresa), Times.Never);
            _empresaRepositoryMock.Verify(x => x.GetByCNPJAsync(empresa.CNPJ), Times.Once);
        }

        [Theory]
        [MemberData(nameof(GetEmpresaInvalida))]
        public async Task CadastroEmpresa_DeveLancarExcecaoComErros_QuandoEmpresaInvalida(Empresa empresaInvalida, IList<string> errosEsperados)
        {
            // Arrange
            _sut = new EmpresaService
            (
                new EmpresaValidator(),
                _empresaRepositoryMock.Object
            );

            // Act & Assert
            var ex = await Assert.ThrowsAsync<BusinessRuleException>(() => 
                _sut.CadastroEmpresaAsync(empresaInvalida));

            Assert.Equal(string.Join(", ", errosEsperados), ex.Message);
        }

        public static IEnumerable<object[]> GetEmpresaInvalida()
        {
            yield return new object[]
            {
                new Empresa()
                {
                    CNPJ = "",
                    NomeResponsavel = "Nome Responsável",
                    EmailResponsavel = "emailresponsavel@gmail.com"
                },

                new List<string> { "O CNPJ da empresa deve ser informado" }
            };

            yield return new object[]
            {
                new Empresa()
                {
                    CNPJ = "1234567890123",
                    NomeResponsavel = "Nome Responsável",
                    EmailResponsavel = "emailresponsavel@gmail.com"
                },

                new List<string> { "O CNPJ deve conter 14 dígitos" }
            };

            yield return new object[]
            {
                new Empresa()
                {
                    CNPJ = "12345678901234",
                    NomeResponsavel = "",
                    EmailResponsavel = "emailresponsavel@gmail.com"
                },

                new List<string> { "O nome do responsável deve ser informado" }
            };

            yield return new object[]
            {
                new Empresa()
                {
                    CNPJ = "12345678901234",
                    NomeResponsavel = "a",
                    EmailResponsavel = "emailresponsavel@gmail.com"
                },

                new List<string> { "O nome do responsável deve ter entre 2 e 100 caracteres" }
            };

            yield return new object[]
            {
                new Empresa()
                {
                    CNPJ = "12345678901234",
                    NomeResponsavel = new string('a', 101),
                    EmailResponsavel = "emailresponsavel@gmail.com"
                },

                new List<string> { "O nome do responsável deve ter entre 2 e 100 caracteres" }
            };

            yield return new object[]
            {
                new Empresa()
                {
                    CNPJ = "12345678901234",
                    NomeResponsavel = "Nome Responsável",
                    EmailResponsavel = ""
                },

                new List<string> { "O email do responsável deve ser informado" }
            };

            yield return new object[]
            {
                new Empresa()
                {
                    CNPJ = "12345678901234",
                    NomeResponsavel = "Nome Responsável",
                    EmailResponsavel = "emailinvalido"
                },

                new List<string> { "O email do responsável deve ser um endereço de email válido" }
            };

            yield return new object[]
            {
                new Empresa()
                {
                    CNPJ = "",
                    NomeResponsavel = "",
                    EmailResponsavel = ""
                },

                new List<string> 
                {
                    "O CNPJ da empresa deve ser informado",
                    "O nome do responsável deve ser informado",
                    "O email do responsável deve ser informado"
                },

            };

            yield return new object[]
            {
                new Empresa()
                {
                    CNPJ = "a",
                    NomeResponsavel = "a",
                    EmailResponsavel = "emailinvalido"
                },

                new List<string> 
                {
                    "O CNPJ deve conter 14 dígitos",
                    "O nome do responsável deve ter entre 2 e 100 caracteres",
                    "O email do responsável deve ser um endereço de email válido" 
                }
            };

            yield return new object[]
            {
                new Empresa()
                {
                    CNPJ = new string('a', 15),
                    NomeResponsavel = new string('a', 101),
                    EmailResponsavel = "emailinvalido"
                },

                new List<string> 
                {
                    "O CNPJ deve conter 14 dígitos",
                    "O nome do responsável deve ter entre 2 e 100 caracteres",
                    "O email do responsável deve ser um endereço de email válido" 
                }
            };
        }
    }
}
