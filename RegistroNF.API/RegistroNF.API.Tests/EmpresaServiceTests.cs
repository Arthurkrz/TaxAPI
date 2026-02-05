using FluentValidation;
using Microsoft.Extensions.Logging;
using Moq;
using RegistroNF.API.Core.Common;
using RegistroNF.API.Core.Contracts.Repository;
using RegistroNF.API.Core.Entities;
using RegistroNF.API.Core.Validators;
using RegistroNF.API.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RegistroNF.API.Tests
{
    public class EmpresaServiceTests
    {
        private readonly Mock<IValidator<Empresa>> _empresaValidatorMock = new();
        private readonly Mock<IEmpresaRepository> _empresaRepositoryMock = new();
        private readonly Mock<ILogger<EmpresaService>> _loggerMock = new();
        private EmpresaService _sut;

        public EmpresaServiceTests()
        {
            _sut = new EmpresaService
            (
                _empresaValidatorMock.Object, 
                _empresaRepositoryMock.Object, 
                _loggerMock.Object
            );
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

            _loggerMock.Verify(
                x => x.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                It.IsAny<Exception>(),
                (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>()),
                Times.Once);
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

            _loggerMock.Verify(
                x => x.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                It.IsAny<Exception>(),
                (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>()),
                Times.Once);
        }

        [Theory]
        [MemberData(nameof(GetEmpresaInvalida))]
        public async Task CadastroEmpresa_DeveLancarExcecaoComErros_QuandoEmpresaInvalida(Empresa empresaInvalida, string erroEsperado)
        {
            // Arrange
            _sut = new EmpresaService
            (
                new EmpresaValidator(),
                _empresaRepositoryMock.Object,
                _loggerMock.Object
            );

            // Act & Assert
            var ex = await Assert.ThrowsAsync<BusinessRuleException>(() => 
                _sut.CadastroEmpresaAsync(empresaInvalida));

            _loggerMock.Verify(
                x => x.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString()!.Contains(erroEsperado)),
                It.IsAny<Exception>(),
                (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>()),
                Times.Once);

            Assert.Equal(erroEsperado, ex.Message);
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

                "O CNPJ da empresa deve ser informado"
            };

            yield return new object[]
            {
                new Empresa()
                {
                    CNPJ = "1234567890123",
                    NomeResponsavel = "Nome Responsável",
                    EmailResponsavel = "emailresponsavel@gmail.com"
                },

                "O CNPJ deve conter 14 dígitos"
            };

            yield return new object[]
            {
                new Empresa()
                {
                    CNPJ = "12345678901234",
                    NomeResponsavel = "",
                    EmailResponsavel = "emailresponsavel@gmail.com"
                },

                "O nome do responsável deve ser informado"
            };

            yield return new object[]
            {
                new Empresa()
                {
                    CNPJ = "12345678901234",
                    NomeResponsavel = "a",
                    EmailResponsavel = "emailresponsavel@gmail.com"
                },

                "O nome do responsável deve ter entre 2 e 100 caracteres"
            };

            yield return new object[]
            {
                new Empresa()
                {
                    CNPJ = "12345678901234",
                    NomeResponsavel = new string('a', 101),
                    EmailResponsavel = "emailresponsavel@gmail.com"
                },

                "O nome do responsável deve ter entre 2 e 100 caracteres"
            };

            yield return new object[]
            {
                new Empresa()
                {
                    CNPJ = "12345678901234",
                    NomeResponsavel = "Nome Responsável",
                    EmailResponsavel = ""
                },

                "O email do responsável deve ser informado"
            };

            yield return new object[]
            {
                new Empresa()
                {
                    CNPJ = "12345678901234",
                    NomeResponsavel = "Nome Responsável",
                    EmailResponsavel = "emailinvalido"
                },

                "O email do responsável deve ser um endereço de email válido"
            };

            yield return new object[]
            {
                new Empresa()
                {
                    CNPJ = "",
                    NomeResponsavel = "",
                    EmailResponsavel = ""
                },

                "O CNPJ da empresa deve ser informado, " +
                "O nome do responsável deve ser informado, " +
                "O email do responsável deve ser informado"
            };

            yield return new object[]
            {
                new Empresa()
                {
                    CNPJ = "a",
                    NomeResponsavel = "a",
                    EmailResponsavel = "emailinvalido"
                },

                "O CNPJ deve conter 14 dígitos, " +
                "O nome do responsável deve ter entre 2 e 100 caracteres, " +
                "O email do responsável deve ser um endereço de email válido" 
            };

            yield return new object[]
            {
                new Empresa()
                {
                    CNPJ = new string('a', 15),
                    NomeResponsavel = new string('a', 101),
                    EmailResponsavel = "emailinvalido"
                },

                "O CNPJ deve conter 14 dígitos, " +
                "O nome do responsável deve ter entre 2 e 100 caracteres, " +
                "O email do responsável deve ser um endereço de email válido"
            };
        }
    }
}
