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
            Assert.Equal(string.Format(LogMessages.NFNUMEROEXISTENTE, nf.Numero, nf.Serie, nf.Empresa.CNPJ), ex.Message);

            _loggerMock.Verify(
                x => x.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString()!.Contains(
                    string.Format(LogMessages.NFNUMEROEXISTENTE, nf.Numero, nf.Serie, nf.Empresa.CNPJ))),
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
            Assert.Equal(string.Join(", ", errosEsperados), ex.Message);

            var valorNumero = notaFiscal.Numero <= 0 ? 
                "não informado" : notaFiscal.Numero.ToString();

            var valorSerie = notaFiscal.Serie <= 0 ?
                "não informada" : notaFiscal.Serie.ToString();

            _loggerMock.Verify(
                x => x.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString()!.Contains(string.Format(
                    LogMessages.NFINVALIDA, valorNumero, valorSerie, string.Join(", ", errosEsperados)))),
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

                string.Format(LogMessages.NFRECENTENUMEROMENOR, 1, "12345678000195")
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

                string.Format(LogMessages.NFANTIGANUMEROMAIOR, 1, "12345678000195")
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

                new List<string>
                {
                    "Erros encontrados na NF de número {0} e série {1}: " +
                      "A série da nota fiscal deve ser maior que zero"
                }
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

                new List<string>
                {
                    "Erros encontrados na NF de número não informado e série 1: " +
                      "O número da nota fiscal deve ser maior que zero"
                }
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

                new List<string>
                {
                    "Erros encontrados na NF de número 1 e série 1: " +
                      "A data de emissão não pode ser futura"
                }
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

                new List<string>
                {
                    "Erros encontrados na NF de número 1 e série 1: " +
                      "O valor bruto dos produtos deve ser maior que zero"
                }
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

                new List<string>
                {
                    "Erros encontrados na NF de número 1 e série 1: " +
                      "O valor total da nota fiscal deve ser igual ao valor bruto dos produtos mais o valor do ICMS"
                }
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

                new List<string>
                {
                    "Erros encontrados na NF de número 1 e série 1: " + 
                      "A empresa emissora da nota fiscal deve ser informada"
                }
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

                new List<string>
                {
                    "Erros encontrados na NF de número não informado e série não informada: " +
                      "O número da nota fiscal deve ser maior que zero, " +
                      "A série da nota fiscal deve ser maior que zero, " +
                      "A data de emissão não pode ser futura, " +
                      "O valor bruto dos produtos deve ser maior que zero, " +
                      "O valor total da nota fiscal deve ser igual ao valor bruto dos produtos mais o valor do ICMS, " +
                      "A empresa emissora da nota fiscal deve ser informada"
                }
            };
        }
    }
}