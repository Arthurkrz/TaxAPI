using FluentValidation;
using Moq;
using RegistroNF.Core.Common;
using RegistroNF.Core.Contracts.Repository;
using RegistroNF.Core.Entities;
using RegistroNF.Core.Validators;
using RegistroNF.Services;
using System.Linq;

namespace RegistroNF.Tests
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
        public void CadastroEmpresa_DeveInvocarMetodoRepositorioCadastroEmpresa()
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

            // Act
            _sut.CadastroEmpresa(empresa);

            // Assert
            _empresaRepositoryMock.Verify(x => x.Cadastrar(empresa), Times.Once);
        }

        [Theory]
        [MemberData(nameof(GetEmpresaInvalida))]
        public void CadastroEmpresa_DeveLancarExcecaoComErros_QuandoEmpresaInvalida(Empresa empresaInvalida, IList<string> errosEsperados)
        {
            // Arrange
            _sut = new EmpresaService
            (
                new EmpresaValidator(),
                _empresaRepositoryMock.Object
            );

            // Act & Assert
            var ex = Assert.Throws<BusinessRuleException>(() => 
                _sut.CadastroEmpresa(empresaInvalida));

            Assert.Equal(string.Join(", ", errosEsperados), ex.Error);
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
