using FluentValidation;
using Microsoft.Extensions.Logging;
using Moq;
using RegistroNF.API.Core.Common;
using RegistroNF.API.Core.Contracts.Repository;
using RegistroNF.API.Core.Contracts.Service;
using RegistroNF.API.Core.Entities;
using RegistroNF.API.Core.Validators;
using RegistroNF.API.Services;

namespace RegistroNF.API.Tests
{
    public class NotaFiscalServiceTests
    {
        private INotaFiscalService _sut;
        private readonly Mock<IValidator<NotaFiscal>> _nfValidatorMock = new();
        private readonly Mock<IEmpresaService> _empresaServiceMock = new();
        private readonly Mock<INotaFiscalRepository> _nfRepositoryMock = new();
        private readonly Mock<ILogger<NotaFiscalService>> _loggerMock = new();

        public NotaFiscalServiceTests()
        {
            _sut = new NotaFiscalService
            (
                _nfValidatorMock.Object,
                _empresaServiceMock.Object,
                _nfRepositoryMock.Object,
                _loggerMock.Object
            );
        }

        [Fact]
        public async Task EmitirNota_DeveInvocarMetodoRepositorioCadastroNota_QuandoEmpresaJaExisteAsync()
        {
            // Arrange
            var id = Guid.NewGuid();

            var empresa = new Empresa()
            {
                Id = id,
                CNPJ = "12345678000195",
                NomeResponsavel = "NomeResponsável",
                EmailResponsavel = "emailresponsavel@gmail.com"
            };

            var notasFiscais = new List<NotaFiscal>
            {
                new NotaFiscal()
                {
                    Numero = 1,
                    Serie = 1,
                    DataEmissao = DateTime.Now.AddDays(-2),
                    ValorBrutoProdutos = 10,
                    ValorICMS = 10,
                    ValorTotalNota = 11,
                    Empresa = empresa
                },

                new NotaFiscal()
                {
                    Numero = 2,
                    Serie = 1,
                    DataEmissao = DateTime.Now.AddDays(-1),
                    ValorBrutoProdutos = 10,
                    ValorICMS = 10,
                    ValorTotalNota = 11,
                    Empresa = empresa
                }
            };

            var nf = new NotaFiscal()
            {
                Numero = 3,
                Serie = 1,
                DataEmissao = DateTime.Now,
                ValorBrutoProdutos = 10,
                ValorICMS = 10,
                ValorTotalNota = 11,
                Empresa = empresa,
                EmpresaId = id
            };

            _nfValidatorMock.Setup(x => x.Validate(
                It.IsAny<NotaFiscal>()))
                .Returns(new FluentValidation.Results.ValidationResult());

            _empresaServiceMock.Setup(x => x.CadastroEmpresaAsync(
                It.IsAny<Empresa>())).ReturnsAsync(empresa);

            _nfRepositoryMock.Setup(x => x.GetSerieNFAsync(
                It.IsAny<string>(), It.IsAny<int>()))
                .ReturnsAsync(new List<NotaFiscal>());

            // Act
            await _sut.EmitirNotaAsync(nf);

            // Assert
            _empresaServiceMock.Verify(x => x.CadastroEmpresaAsync(empresa), Times.Once);
            _nfRepositoryMock.Verify(x => x.Create(nf), Times.Once);

            _loggerMock.Verify(
                x => x.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                It.IsAny<Exception>(),
                (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>()),
                Times.Never);

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
        public async Task EmitirNota_DeveInvocarMetodosRepositorioCadastroNotaEmpresaAsync()
        {
            // Arrange
            var id = Guid.NewGuid();

            var empresa = new Empresa()
            {
                Id = id,
                CNPJ = "12345678000195",
                NomeResponsavel = "NomeResponsável",
                EmailResponsavel = "emailresponsavel@gmail.com"
            };
            
            var nf = new NotaFiscal()
            {
                Numero = 1,
                Serie = 1,
                DataEmissao = DateTime.Now,
                ValorBrutoProdutos = 10,
                ValorICMS = 10,
                ValorTotalNota = 20,
                Empresa = empresa,
                EmpresaId = id
            };

            _nfValidatorMock.Setup(x => x.Validate(
                It.IsAny<NotaFiscal>()))
                .Returns(new FluentValidation.Results.ValidationResult());

            _empresaServiceMock.Setup(x => x.CadastroEmpresaAsync(
                It.IsAny<Empresa>())).ReturnsAsync(empresa);

            _nfRepositoryMock.Setup(x => x.GetSerieNFAsync(
                It.IsAny<string>(), It.IsAny<int>()))
                .ReturnsAsync(new List<NotaFiscal>());

            // Act
            await _sut.EmitirNotaAsync(nf);

            // Assert
            _empresaServiceMock.Verify(x => x.CadastroEmpresaAsync(empresa), Times.Once);
            _nfRepositoryMock.Verify(x => x.Create(nf), Times.Once);

            _loggerMock.Verify(
                x => x.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                It.IsAny<Exception>(),
                (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>()),
                Times.Never);

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
        public async Task EmitirNota_DeveLancarExcecao_QuandoNotaDeMesmoNumeroJaExiste()
        {
            // Arrange
            var empresa = new Empresa()
            {
                CNPJ = "12345678000195",
                NomeResponsavel = "NomeResponsável",
                EmailResponsavel = "emailresponsavel@gmail.com"
            };

            var nf = new NotaFiscal()
            {
                Serie = 1,
                Numero = 1,
                DataEmissao = DateTime.Now,
                ValorBrutoProdutos = 10,
                ValorICMS = 10,
                ValorTotalNota = 20,
                Empresa = empresa
            };

            _nfValidatorMock.Setup(x => x.Validate(nf)).Returns(
                new FluentValidation.Results.ValidationResult());

            _nfRepositoryMock.Setup(x => x.GetSerieNFAsync(
                It.IsAny<string>(), It.IsAny<int>()))
                .ReturnsAsync(new List<NotaFiscal>
                {
                    new NotaFiscal()
                    {
                        Serie = 1,
                        Numero = 1,
                        DataEmissao = DateTime.Now,
                        ValorBrutoProdutos = 10,
                        ValorICMS = 10,
                        ValorTotalNota = 20,
                        Empresa = empresa
                    }
                });

            // Act & Assert
            var ex = await Assert.ThrowsAsync<BusinessRuleException>(() => _sut.EmitirNotaAsync(nf));
            var error = LogMessages.NFNUMEROEXISTENTE.Replace("{numero}", nf.Numero.ToString());

            Assert.Equal(error, ex.Message);

            _loggerMock.Verify(
                x => x.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString()!.Contains(error)),
                It.IsAny<Exception>(),
                (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>()),
                Times.Once);
        }

        [Theory]
        [MemberData(nameof(GetNotaInvalidaComErro))]
        public async Task EmitirNota_DeveLancarExcecaoComErros_QuandoNotaInvalida(NotaFiscal notaFiscal, string erroEsperado)
        {
            // Arrange
            _sut = new NotaFiscalService
            (
                new NotaFiscalValidator(),
                _empresaServiceMock.Object,
                _nfRepositoryMock.Object,
                _loggerMock.Object
            );

            _nfRepositoryMock.Setup(x => x.GetSerieNFAsync(
                It.IsAny<string>(), It.IsAny<int>()))
                .ReturnsAsync(new List<NotaFiscal>());

            // Act & Assert
            var ex = await Assert.ThrowsAsync<BusinessRuleException>(() => _sut.EmitirNotaAsync(notaFiscal));
            Assert.Equal(erroEsperado, ex.Message);

            _loggerMock.Verify(
                x => x.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString()!.Contains(erroEsperado)),
                It.IsAny<Exception>(),
                (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>()),
                Times.Once);
        }

        [Theory]
        [MemberData(nameof(GetNFOrdemInvalida))]
        public async Task EmitirNota_DeveLancarExcecao_QuandoOrdemIncorreta(List<NotaFiscal> nfsJaExistentes, NotaFiscal nfNova, string erroEsperado)
        {
            // Arrange
            _nfValidatorMock.Setup(x => x.Validate(It.IsAny<NotaFiscal>()))
                .Returns(new FluentValidation.Results.ValidationResult());

            _nfRepositoryMock.Setup(x => x.GetSerieNFAsync(
                It.IsAny<string>(), It.IsAny<int>()))
                .ReturnsAsync(nfsJaExistentes);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<BusinessRuleException>(() => _sut.EmitirNotaAsync(nfNova));
            Assert.Equal(erroEsperado, ex.Message);

            _loggerMock.Verify(
                x => x.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString()!.Contains(erroEsperado)),
                It.IsAny<Exception>(),
                (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>()),
                Times.Once);
        }

        public static IEnumerable<object[]> GetNFOrdemInvalida()
        {
            yield return new object[]
            {
                new List<NotaFiscal>
                {
                    new NotaFiscal()
                    {
                        Serie = 1,
                        Numero = 1,
                        DataEmissao = new DateTime(2025, 09, 24),
                        Empresa = new Empresa()
                        {
                            CNPJ = "12345678000195",
                            NomeResponsavel = "aaa",
                            EmailResponsavel = "aaa"
                        }
                    },

                    new NotaFiscal()
                    {
                        Serie = 1,
                        Numero = 2,
                        DataEmissao = new DateTime(2025, 09, 25),
                        Empresa = new Empresa()
                        {
                            CNPJ = "12345678000195",
                            NomeResponsavel = "aaa",
                            EmailResponsavel = "aaa"
                        }
                    }
                },

                new NotaFiscal()
                {
                    Serie = 1,
                    Numero = 3,
                    DataEmissao = new DateTime(2025, 09, 23),
                    Empresa = new Empresa()
                    {
                        CNPJ = "12345678000195",
                        NomeResponsavel = "aaa",
                        EmailResponsavel = "aaa"
                    }
                },

                LogMessages.NFRECENTENUMEROMENOR.Replace("{serie}", "1")
            };

            yield return new object[]
            {
                new List<NotaFiscal>
                {
                    new NotaFiscal()
                    {
                        Serie = 1,
                        Numero = 2,
                        DataEmissao = new DateTime(2025, 09, 25),
                        Empresa = new Empresa()
                        {
                            CNPJ = "12345678000195",
                            NomeResponsavel = "aaa",
                            EmailResponsavel = "aaa"
                        }
                    },

                    new NotaFiscal()
                    {
                        Serie = 1,
                        Numero = 3,
                        DataEmissao = new DateTime(2025, 09, 26),
                        Empresa = new Empresa()
                        {
                            CNPJ = "12345678000195",
                            NomeResponsavel = "aaa",
                            EmailResponsavel = "aaa"
                        }
                    }
                },

                new NotaFiscal()
                {
                    Serie = 1,
                    Numero = 1,
                    DataEmissao = new DateTime(2025, 09, 27),
                    Empresa = new Empresa()
                    {
                        CNPJ = "12345678000195",
                        NomeResponsavel = "aaa",
                        EmailResponsavel = "aaa"
                    }
                },

                LogMessages.NFANTIGANUMEROMAIOR.Replace("{serie}", "1")
            };
        }

        public static IEnumerable<object[]> GetNotaInvalidaComErro()
        {
            var empresa = new Empresa()
            {
                CNPJ = "12345678000195",
                NomeResponsavel = "NomeResponsável",
                EmailResponsavel = "emailresponsavel@gmail.com"
            };

            yield return new object[]
            {
                new NotaFiscal()
                {
                    Serie = 0,
                    Numero = 1,
                    DataEmissao = DateTime.Now.AddDays(-1),
                    ValorBrutoProdutos = 10,
                    ValorICMS = 10,
                    ValorTotalNota = 11,
                    Empresa = empresa
                },

                LogMessages.SERIEMAIORQUEZERO
            };

            yield return new object[]
            {
                new NotaFiscal()
                {
                    Serie = 1,
                    Numero = 0,
                    DataEmissao = DateTime.Now.AddDays(-1),
                    ValorBrutoProdutos = 10,
                    ValorICMS = 10,
                    ValorTotalNota = 11,
                    Empresa = empresa
                },

                LogMessages.NUMEROMAIORQUEZERO
            };

            yield return new object[]
            {
                new NotaFiscal()
                {
                    Serie = 1,
                    Numero = 1,
                    DataEmissao = DateTime.Now.AddDays(1),
                    ValorBrutoProdutos = 10,
                    ValorICMS = 10,
                    ValorTotalNota = 11,
                    Empresa = empresa
                },

                LogMessages.DATAEMISSAONAOFUTURA
            };

            yield return new object[]
            {
                new NotaFiscal()
                {
                    Serie = 1,
                    Numero = 1,
                    DataEmissao = DateTime.Now.AddDays(-1),
                    ValorBrutoProdutos = 0,
                    ValorICMS = 10,
                    ValorTotalNota = 0,
                    Empresa = empresa
                },

                LogMessages.VALORBRUTOMAIORQUEZERO
            };

            yield return new object[]
            {
                new NotaFiscal()
                {
                    Serie = 1,
                    Numero = 1,
                    DataEmissao = DateTime.Now.AddDays(-1),
                    ValorBrutoProdutos = 10,
                    ValorICMS = 10,
                    ValorTotalNota = 0,
                    Empresa = empresa
                },

                LogMessages.VALORTOTALINVALIDO
            };

            yield return new object[]
            {
                new NotaFiscal()
                {
                    Serie = 1,
                    Numero = 1,
                    DataEmissao = DateTime.Now.AddDays(-1),
                    ValorBrutoProdutos = 10,
                    ValorICMS = 10,
                    ValorTotalNota = 11,
                    Empresa = null!
                },

                LogMessages.EMPRESANAOINFORMADA
            };

            yield return new object[]
            {
                new NotaFiscal()
                {
                    Serie = 0,
                    Numero = 0,
                    DataEmissao = DateTime.Now.AddDays(1),
                    ValorBrutoProdutos = 0,
                    ValorICMS = 0,
                    ValorTotalNota = -1,
                    Empresa = null!
                },

                string.Join(", ", new List<string>
                {
                    LogMessages.NUMEROMAIORQUEZERO,
                    LogMessages.SERIEMAIORQUEZERO,
                    LogMessages.DATAEMISSAONAOFUTURA,
                    LogMessages.VALORBRUTOMAIORQUEZERO,
                    LogMessages.VALORTOTALINVALIDO,
                    LogMessages.EMPRESANAOINFORMADA
                })
            };
        }
    }
}