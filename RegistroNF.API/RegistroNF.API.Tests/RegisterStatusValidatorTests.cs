using RegistroNF.API.Core.Entities;
using RegistroNF.API.Core.Enum;
using RegistroNF.API.Services.Utilities;

namespace RegistroNF.API.Tests
{
    public class RegisterStatusValidatorTests
    {
        [Theory]
        [MemberData(nameof(GetEntitiesValidatorRegisterStatus))]
        public void ValidateRegisterStatus_DeveRetornarStatusCorretoEmpresa(Entity entity, Status statusEsperado)
        {
            // Act
            var result = RegisterStatusValidator.ValidateRegisterStatus(entity);

            // Assert
            Assert.Equal(statusEsperado, result);   
        }

        public static IEnumerable<object[]> GetEntitiesValidatorRegisterStatus()
        {
            yield return new object[]
            {
                new Empresa()
                {
                    CNPJ = "12345678000185",
                    NomeResponsavel = "Nome Responsavel",
                    EmailResponsavel = "teste@teste.com",
                    RazaoSocial = "Razao Social",
                    NomeFantasia = "Nome Fantasia",
                    Endereco = new Endereco()
                },

                Status.Completo
            };

            yield return new object[]
            {
                new Endereco()
                {
                    Municipio = "Municipio",
                    Logradouro = "Logradouro",
                    Numero = 123,
                    CEP = 12345678,
                    UF = "UF"
                },

                Status.Completo
            };

            yield return new object[]
            {
                new Endereco()
                {
                    Municipio = "Municipio",
                    Logradouro = "Logradouro",
                    UF = "UF"
                },

                Status.Parcial
            };

            yield return new object[]
            {
                new Empresa()
                {
                    CNPJ = default!,
                    NomeResponsavel = default!,
                    EmailResponsavel = default!,
                    RazaoSocial = default!,
                    NomeFantasia = default!,
                    Endereco = new Endereco()
                },

                Status.Parcial
            };

            yield return new object[]
            {
                new Endereco()
                {
                    Numero = 123,
                    CEP = 12345678
                },

                Status.Parcial
            };
        }
    }
}
