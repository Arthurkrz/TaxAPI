using FluentValidation;
using Microsoft.Extensions.Logging;
using Moq;
using RegistroNF.API.Core.Common;
using RegistroNF.API.Core.Contracts.Repository;
using RegistroNF.API.Core.Contracts.SMTPService;
using RegistroNF.API.Core.Entities;
using RegistroNF.API.Core.Validators;
using RegistroNF.API.Services;

namespace RegistroNF.API.Tests
{
    public class EmpresaServiceTests
    {
        private readonly Mock<IValidator<Empresa>> _empresaValidatorMock = new();
        private readonly Mock<IEmpresaRepository> _empresaRepositoryMock = new();
        private readonly Mock<IEmailService> _emailServiceMock = new();
        private readonly Mock<ILogger<EmpresaService>> _loggerMock = new();
        private EmpresaService _sut;

        public EmpresaServiceTests()
        {
            _sut = new EmpresaService
            (
                _empresaValidatorMock.Object, 
                _empresaRepositoryMock.Object, 
                _emailServiceMock.Object,
                _loggerMock.Object
            );
        }

        [Theory]
        [MemberData(nameof(GetEmpresaCompletaEParcial))]
        public async Task CadastroEmpresaAsync_DeveInvocarMetodoRepositorioCadastroEmpresa_SeEmpresaNaoExisteAsync(Empresa empresa)
        {
            // Arrange
            _empresaValidatorMock.Setup(x => x.Validate(empresa))
                .Returns(new FluentValidation.Results.ValidationResult());

            _empresaRepositoryMock.Setup(x => x.EhExistenteAsync(
                It.IsAny<string>())).ReturnsAsync(false);

            // Act
            await _sut.CadastroEmpresaAsync(empresa);

            // Assert
            _empresaRepositoryMock.Verify(x => x.CreateAsync(empresa), Times.Once);
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
        public async Task CadastroEmpresaAsync_NaoDeveInvocarMetodoRepositorioCadastro_SeEmpresaExisteAsync()
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
            _empresaRepositoryMock.Verify(x => x.CreateAsync(empresa), Times.Never);
            _empresaRepositoryMock.Verify(x => x.GetByCNPJAsync(empresa.CNPJ), Times.Once);
            _emailServiceMock.Verify(x => x.SendEmailAsync(It.IsAny<EmailMessage>()), Times.Never);

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
        public async Task CadastroEmpresaAsync_DeveLancarExcecaoComErros_QuandoEmpresaInvalida(Empresa empresaInvalida, string erroEsperado)
        {
            // Arrange
            _sut = new EmpresaService
            (
                new EmpresaValidator(),
                _empresaRepositoryMock.Object,
                _emailServiceMock.Object,
                _loggerMock.Object
            );

            // Act & Assert
            var ex = await Assert.ThrowsAsync<BusinessRuleException>(() => 
                _sut.CadastroEmpresaAsync(empresaInvalida));

            Assert.Equal(erroEsperado, ex.Message);

            _empresaRepositoryMock.Verify(x => x.CreateAsync(empresaInvalida), Times.Never);
            _emailServiceMock.Verify(x => x.SendEmailAsync(It.IsAny<EmailMessage>()), Times.Never);
            _empresaRepositoryMock.Verify(x => x.GetByCNPJAsync(empresaInvalida.CNPJ), Times.Never);

            _loggerMock.Verify(
                x => x.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString()!.Contains(erroEsperado)),
                It.IsAny<Exception>(),
                (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>()),
                Times.Once);

            _loggerMock.Verify(
                x => x.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                It.IsAny<Exception>(),
                (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>()),
                Times.Never);
        }

        [Fact]
        public async Task UpdateStatusCadastroAsync_DeveAtualizarCadastro_QuandoEmpresaCompletaAsync()
        {
            // Arrange
            var empresa = new Empresa()
            {
                CNPJ = "12345678000173",
                NomeResponsavel = "Nome Responsavel",
                EmailResponsavel = "teste@teste.com",
                RazaoSocial = "Razao Social",
                NomeFantasia = "Nome Fantasia",
                Endereco = new Endereco()
                {
                    Municipio = "Municipio",
                    Logradouro = "Logradouro",
                    Numero = 123,
                    CEP = 123456789,
                    UF = "UF",
                }
            };

            _empresaRepositoryMock.Setup(x => x.GetByCNPJAsync(
                It.IsAny<string>())).ReturnsAsync(empresa);

            _empresaValidatorMock.Setup(x => x.Validate(empresa))
                .Returns(new FluentValidation.Results.ValidationResult());

            // Act
            await _sut.UpdateStatusCadastroAsync(empresa);

            // Assert
            _empresaRepositoryMock.Verify(x => x.UpdateAsync(
                It.IsAny<Empresa>()), Times.Once());

            _emailServiceMock.Verify(x => x.SendEmailAsync(It.IsAny<EmailMessage>()), Times.Once);

            _loggerMock.Verify(
                x => x.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                It.IsAny<Exception>(),
                (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>()),
                Times.Once);

            _loggerMock.Verify(
                x => x.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                It.IsAny<Exception>(),
                (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>()),
                Times.Never);
        }

        [Fact]
        public async Task UpdateStatusCadastroAsync_DeveLancarExcecao_QuandoEmpresaParcialAsync()
        {
            // Arrange
            var empresa = new Empresa()
            {
                CNPJ = default!,
                NomeResponsavel = default!,
                EmailResponsavel = default!,
                RazaoSocial = default!,
                NomeFantasia = default!,
                Endereco = new Endereco()
                {
                    Municipio = default!,
                    Logradouro = default!,
                    Numero = default!,
                    CEP = default!,
                    UF = default!
                }
            };

            _empresaRepositoryMock.Setup(x => x.GetByCNPJAsync(
                It.IsAny<string>())).ReturnsAsync(empresa);

            _empresaValidatorMock.Setup(x => x.Validate(empresa))
                .Returns(new FluentValidation.Results.ValidationResult());

            // Act & Assert
            var ex = await Assert.ThrowsAsync<BusinessRuleException>(() => 
                _sut.UpdateStatusCadastroAsync(empresa));

            Assert.Equal(LogMessages.NOVAEMPRESAPARCIAL, ex.Message);

            _empresaRepositoryMock.Verify(x => x.UpdateAsync(
                It.IsAny<Empresa>()), Times.Never());

            _emailServiceMock.Verify(x => x.SendEmailAsync(
                It.IsAny<EmailMessage>()), Times.Never());

            _loggerMock.Verify(
                x => x.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                It.IsAny<Exception>(),
                (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>()),
                Times.Never);

            _loggerMock.Verify(
                x => x.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                It.IsAny<Exception>(),
                (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>()),
                Times.Once);
        }

        [Fact]
        public async Task UpdateStatusCadastroAsync_DeveLancarExcecao_QuandoEmpresaOriginalNaoEncontradaAsync()
        {
            // Arrange
            var empresa = new Empresa()
            {
                CNPJ = "12345678000173",
                NomeResponsavel = "Nome Responsavel",
                EmailResponsavel = "teste@teste.com",
                RazaoSocial = "Razao Social",
                NomeFantasia = "Nome Fantasia",
                Endereco = new Endereco()
                {
                    Municipio = "Municipio",
                    Logradouro = "Logradouro",
                    Numero = 123,
                    CEP = 123456789,
                    UF = "UF",
                }
            };

            _empresaRepositoryMock.Setup(x => x.GetByCNPJAsync(It.IsAny<string>()));

            _empresaValidatorMock.Setup(x => x.Validate(empresa))
                .Returns(new FluentValidation.Results.ValidationResult());

            // Act & Assert
            var ex = await Assert.ThrowsAsync<BusinessRuleException>(() =>
                _sut.UpdateStatusCadastroAsync(empresa));

            Assert.Equal(LogMessages.EMPRESANAOENCONTRADA, ex.Message);

            _empresaRepositoryMock.Verify(x => x.UpdateAsync(
                It.IsAny<Empresa>()), Times.Never());

            _emailServiceMock.Verify(x => x.SendEmailAsync(
                It.IsAny<EmailMessage>()), Times.Never());

            _loggerMock.Verify(
                x => x.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                It.IsAny<Exception>(),
                (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>()),
                Times.Never);

            _loggerMock.Verify(
                x => x.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                It.IsAny<Exception>(),
                (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>()),
                Times.Once);
        }

        [Theory]
        [MemberData(nameof(GetEmpresaInvalida))]
        public async Task UpdateStatusCadastroAsync_DeveLancarExcecaoComErros_QuandoEmpresaInvalidaAsync(Empresa empresaInvalida, string erroEsperado)
        {
            // Arrange
            _sut = new EmpresaService
            (
                new EmpresaValidator(),
                _empresaRepositoryMock.Object,
                _emailServiceMock.Object,
                _loggerMock.Object
            );

            _empresaRepositoryMock.Setup(x => x.GetByCNPJAsync(
                It.IsAny<string>())).ReturnsAsync(empresaInvalida);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<BusinessRuleException>(() =>
                _sut.UpdateStatusCadastroAsync(empresaInvalida));

            Assert.Equal(erroEsperado, ex.Message);

            _empresaRepositoryMock.Verify(x => x.UpdateAsync(
                It.IsAny<Empresa>()), Times.Never());

            _emailServiceMock.Verify(x => x.SendEmailAsync(
                It.IsAny<EmailMessage>()), Times.Never());

            _loggerMock.Verify(
                x => x.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString()!.Contains(erroEsperado)),
                It.IsAny<Exception>(),
                (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>()),
                Times.Once);

            _loggerMock.Verify(
                x => x.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                It.IsAny<Exception>(),
                (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>()),
                Times.Never);
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

                LogMessages.CNPJNAOINFORMADO
            };

            yield return new object[]
            {
                new Empresa()
                {
                    CNPJ = "1234567890123",
                    NomeResponsavel = "Nome Responsável",
                    EmailResponsavel = "emailresponsavel@gmail.com"
                },

                LogMessages.CNPJINVALIDO
            };

            yield return new object[]
            {
                new Empresa()
                {
                    CNPJ = "12345678901234",
                    NomeResponsavel = "",
                    EmailResponsavel = "emailresponsavel@gmail.com"
                },

                LogMessages.NOMERESPONSAVELNAOINFORMADO
            };

            yield return new object[]
            {
                new Empresa()
                {
                    CNPJ = "12345678901234",
                    NomeResponsavel = "a",
                    EmailResponsavel = "emailresponsavel@gmail.com"
                },

                LogMessages.NOMERESPONSAVELINVALIDO
            };

            yield return new object[]
            {
                new Empresa()
                {
                    CNPJ = "12345678901234",
                    NomeResponsavel = new string('a', 101),
                    EmailResponsavel = "emailresponsavel@gmail.com"
                },

                LogMessages.NOMERESPONSAVELINVALIDO
            };

            yield return new object[]
            {
                new Empresa()
                {
                    CNPJ = "12345678901234",
                    NomeResponsavel = "Nome Responsável",
                    EmailResponsavel = ""
                },

                LogMessages.EMAILRESPONSAVELNAOINFORMADO
            };

            yield return new object[]
            {
                new Empresa()
                {
                    CNPJ = "12345678901234",
                    NomeResponsavel = "Nome Responsável",
                    EmailResponsavel = "emailinvalido"
                },

                LogMessages.EMAILRESPONSAVELINVALIDO
            };

            yield return new object[]
            {
                new Empresa()
                {
                    CNPJ = "",
                    NomeResponsavel = "",
                    EmailResponsavel = ""
                },

                string.Join(", ", new List<string>
                {
                    LogMessages.CNPJNAOINFORMADO,
                    LogMessages.NOMERESPONSAVELNAOINFORMADO,
                    LogMessages.EMAILRESPONSAVELNAOINFORMADO
                })
            };

            yield return new object[]
            {
                new Empresa()
                {
                    CNPJ = "a",
                    NomeResponsavel = "a",
                    EmailResponsavel = "emailinvalido"
                },

                string.Join(", ", new List<string>
                {
                    LogMessages.CNPJINVALIDO,
                    LogMessages.NOMERESPONSAVELINVALIDO,
                    LogMessages.EMAILRESPONSAVELINVALIDO
                })
            };

            yield return new object[]
            {
                new Empresa()
                {
                    CNPJ = new string('a', 15),
                    NomeResponsavel = new string('a', 101),
                    EmailResponsavel = "emailinvalido"
                },

                string.Join(", ", new List<string>
                {
                    LogMessages.CNPJINVALIDO,
                    LogMessages.NOMERESPONSAVELINVALIDO,
                    LogMessages.EMAILRESPONSAVELINVALIDO
                })
            };
        }

        public static IEnumerable<object[]> GetEmpresaCompletaEParcial()
        {
            yield return new object[]
            {
                new Empresa()
                {
                    CNPJ = "12345678000199",
                    NomeResponsavel = "Nome Responsável",
                    EmailResponsavel = "emailresponsavel@gmail.com",
                    RazaoSocial = "Razão Social",
                    NomeFantasia = "Nome Fantasia",
                    Endereco = new Endereco()
                    {
                        Municipio = "Município",
                        Logradouro = "Logradouro",
                        Numero = 123,
                        CEP = 123456789,
                        UF = "UF"
                    } 
                }
            };

            yield return new object[]
            {
                new Empresa()
                {
                    CNPJ = "12345678000199",
                    NomeResponsavel = "Nome Responsável",
                    EmailResponsavel = "emailresponsavel@gmail.com",
                    RazaoSocial = "Razão Social",
                    NomeFantasia = "Nome Fantasia"
                }
            };

            yield return new object[]
            {
                new Empresa()
                {
                    CNPJ = "12345678000199",
                    NomeResponsavel = "Nome Responsável",
                    EmailResponsavel = "emailresponsavel@gmail.com",
                }
            };
        }
    }
}
