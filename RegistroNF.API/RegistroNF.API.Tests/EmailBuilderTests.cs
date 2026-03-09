using RegistroNF.API.Core.Common;
using RegistroNF.API.Core.Entities;
using RegistroNF.API.Services.Utilities;

namespace RegistroNF.API.Tests
{
    public class EmailBuilderTests
    {
        [Fact]
        public void BuildEmailCompleto_DeveRetornarEmailCorreto()
        {
            // Arrange
            var empresa = new Empresa()
            {

            };

            // Act
            var result = EmailBuilder.BuildEmailCompleto(empresa);

            // Assert
            Assert.Equal(empresa.EmailResponsavel, result.To.First());
            Assert.Equal(EmailTemplate.EMPRESANOVACOMPLETASUBJECT, result.Subject);
            Assert.Equal(emailCompletoEsperado, result.Content);
        }

        [Fact]
        public void BuildEmailParcial_DeveRetornarEmailCorreto()
        {
            // Arrange
            var empresa = new Empresa()
            {

            };

            // Act
            var result = EmailBuilder.BuildEmailParcial(empresa);

            // Assert
            Assert.Equal(empresa.EmailResponsavel, result.To.First());
            Assert.Equal(EmailTemplate.EMPRESANOVAPARCIALSUBJECT, result.Subject);
            Assert.Equal(emailParcialEsperado, result.Content);
        }

        [Fact]
        public void BuildEmailNotificado_DeveRetornarEmailCorreto()
        {
            // Arrange
            var empresa = new Empresa()
            {

            };

            // Act
            var result = EmailBuilder.BuildEmailNotificado(empresa);

            // Assert
            Assert.Equal(empresa.EmailResponsavel, result.To.First());
            Assert.Equal(EmailTemplate.EMPRESANOTIFICADASUBJECT, result.Subject);
            Assert.Equal(emailNotificadoEsperado, result.Content);
        }

        [Fact]
        public void BuildEmailBloqueado_DeveRetornarEmailCorreto()
        {
            // Arrange
            var empresa = new Empresa()
            {

            };

            // Act
            var result = EmailBuilder.BuildEmailBloqueado(empresa);

            // Assert
            Assert.Equal(empresa.EmailResponsavel, result.To.First());
            Assert.Equal(EmailTemplate.EMPRESABLOQUEADASUBJECT, result.Subject);
            Assert.Equal(emailBloqueadoEsperado, result.Content);
        }

        private string emailCompletoEsperado = @"";

        private string emailParcialEsperado = @"";

        private string emailNotificadoEsperado = @"";

        private string emailBloqueadoEsperado = @"";
    }
}
