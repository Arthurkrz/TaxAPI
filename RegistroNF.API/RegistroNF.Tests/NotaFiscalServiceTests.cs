using FluentValidation;
using Moq;
using RegistroNF.Core.Common;
using RegistroNF.Core.Contracts.Repository;
using RegistroNF.Core.Contracts.Service;
using RegistroNF.Core.Entities;
using RegistroNF.Core.Validators;
using TaxAPI.Services;

namespace RegistroNF.Tests
{
    public class NotaFiscalServiceTests
    {
        private INotaFiscalService _sut;
        private readonly Mock<IValidator<NotaFiscal>> _nfValidatorMock;
        private readonly Mock<IEmpresaService> _empresaServiceMock;
        private readonly Mock<INotaFiscalRepository> _nfRepositoryMock;

        public NotaFiscalServiceTests()
        {
            _nfValidatorMock = new Mock<IValidator<NotaFiscal>>();
            _empresaServiceMock = new Mock<IEmpresaService>();
            _nfRepositoryMock = new Mock<INotaFiscalRepository>();

            _sut = new NotaFiscalService
            (
                _nfValidatorMock.Object,
                _empresaServiceMock.Object,
                _nfRepositoryMock.Object
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

            // Act
            await _sut.EmitirNotaAsync(nf);

            // Assert
            _empresaServiceMock.Verify(x => x.CadastroEmpresaAsync(empresa), Times.Once);
            _nfRepositoryMock.Verify(x => x.Create(nf), Times.Once);
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

            // Act
            await _sut.EmitirNotaAsync(nf);

            // Assert
            _empresaServiceMock.Verify(x => x.CadastroEmpresaAsync(empresa), Times.Once);
            _nfRepositoryMock.Verify(x => x.Create(nf), Times.Once);
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
            Assert.Equal(ErrorMessages.NFNUMEROEXISTENTE, ex.Message);
        }

        [Theory]
        [MemberData(nameof(GetNotaInvalidaComErro))]
        public async Task EmitirNota_DeveLancarExcecaoComErros_QuandoNotaInvalida(NotaFiscal notaFiscal, List<string> errosEsperados)
        {
            // Arrange
            _sut = new NotaFiscalService
            (
                new NotaFiscalValidator(),
                _empresaServiceMock.Object,
                _nfRepositoryMock.Object
            );

            _nfRepositoryMock.Setup(x => x.GetSerieNFAsync(
                It.IsAny<string>(), It.IsAny<int>()))
                .ReturnsAsync(new List<NotaFiscal>());

            // Act & Assert
            var ex = await Assert.ThrowsAsync<BusinessRuleException>(() => _sut.EmitirNotaAsync(notaFiscal));
            Assert.Equal(string.Join(", ", errosEsperados), ex.Message);
        }

        [Theory]
        [MemberData(nameof(GetNFOrdemInvalida))]
        public async Task EmitirNota_DeveLancarExcecao_QuandoOrdemIncorreta(List<NotaFiscal> nfsJaExistentes, NotaFiscal nfNova)
        {
            // Arrange
            _nfValidatorMock.Setup(x => x.Validate(It.IsAny<NotaFiscal>()))
                .Returns(new FluentValidation.Results.ValidationResult());

            _nfRepositoryMock.Setup(x => x.GetSerieNFAsync(
                It.IsAny<string>(), It.IsAny<int>()))
                .ReturnsAsync(nfsJaExistentes);

            // Act & Assert
            await Assert.ThrowsAsync<BusinessRuleException>(() => _sut.EmitirNotaAsync(nfNova));
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
                }
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
                }
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
                    "O número da nota fiscal deve ser maior que zero",
                    "A série da nota fiscal deve ser maior que zero",
                    "A data de emissão não pode ser futura",
                    "O valor bruto dos produtos deve ser maior que zero",
                    "O valor total da nota fiscal deve ser igual ao valor bruto dos produtos mais o valor do ICMS",
                    "A empresa emissora da nota fiscal deve ser informada"
                }
            };
        }
    }
}