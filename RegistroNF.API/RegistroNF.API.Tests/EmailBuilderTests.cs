using RegistroNF.API.Core.Common;
using RegistroNF.API.Core.Entities;
using RegistroNF.API.Core.Enum;
using RegistroNF.API.Services.Utilities;
using System.Text.RegularExpressions;

namespace RegistroNF.API.Tests
{
    public class EmailBuilderTests
    {
        [Fact]
        public void BuildEmailCompleto_DeveRetornarEmailCorreto()
        {
            // Arrange
            SystemTime.Reset();
            var date = new DateTime(2026, 1, 1, 10, 0, 0);
            SystemTime.Now = () => date;

            var empresa = new Empresa()
            {
                CNPJ = "14983872000148",
                NomeResponsavel = "Nome Responsável",
                EmailResponsavel = "emailresponsavel@gmail.com",
                RazaoSocial = "RazaoSocial LTDA",
                NomeFantasia = "Nome Fantasia",
                Endereco = new Endereco()
                {
                    Municipio = "Municipio",
                    Logradouro = "Logradouro",
                    Numero = 123,
                    CEP = 12345678,
                    UF = "UF",
                },
                Status = Status.Completo
            };

            // Act
            var result = EmailBuilder.BuildEmailCompleto(empresa);

            // Assert
            Assert.Equal(empresa.EmailResponsavel, result.To.First());
            Assert.Equal(EmailTemplate.EMPRESANOVACOMPLETASUBJECT, result.Subject);
            Assert.Equal(NormalizeHTML(emailCompletoEsperado), NormalizeHTML(result.Content));
        }

        [Fact]
        public void BuildEmailParcial_DeveRetornarEmailCorreto()
        {
            // Arrange
            SystemTime.Reset();
            var date = new DateTime(2026, 1, 1, 10, 0, 0);
            SystemTime.Now = () => date;

            var empresa = new Empresa()
            {
                CNPJ = "14983872000148",
                NomeResponsavel = "Nome Responsável",
                EmailResponsavel = "emailresponsavel@gmail.com",
                RazaoSocial = "RazaoSocial LTDA",
                NomeFantasia = "Nome Fantasia",
                Endereco = new Endereco(),
                Status = Status.Completo
            };

            // Act
            var result = EmailBuilder.BuildEmailParcial(empresa);

            // Assert
            Assert.Equal(empresa.EmailResponsavel, result.To.First());
            Assert.Equal(EmailTemplate.EMPRESANOVAPARCIALSUBJECT, result.Subject);
            Assert.Equal(NormalizeHTML(emailParcialEsperado), NormalizeHTML(result.Content));
        }

        [Fact]
        public void BuildEmailNotificado_DeveRetornarEmailCorreto()
        {
            // Arrange
            SystemTime.Reset();
            var date = new DateTime(2026, 1, 1, 10, 0, 0);
            SystemTime.Now = () => date;

            var empresa = new Empresa()
            {
                CNPJ = "14983872000148",
                NomeResponsavel = "Nome Responsável",
                EmailResponsavel = "emailresponsavel@gmail.com",
                RazaoSocial = "RazaoSocial LTDA",
                NomeFantasia = "Nome Fantasia",
                Endereco = new Endereco(),
                Status = Status.Completo
            };

            // Act
            var result = EmailBuilder.BuildEmailNotificado(empresa);

            // Assert
            Assert.Equal(empresa.EmailResponsavel, result.To.First());
            Assert.Equal(EmailTemplate.EMPRESANOTIFICADASUBJECT, result.Subject);
            Assert.Equal(NormalizeHTML(emailNotificadoEsperado), NormalizeHTML(result.Content));
        }

        [Fact]
        public void BuildEmailBloqueado_DeveRetornarEmailCorreto()
        {
            // Arrange
            SystemTime.Reset();
            var date = new DateTime(2026, 1, 1, 10, 0, 0);
            SystemTime.Now = () => date;

            var empresa = new Empresa()
            {
                CNPJ = "14983872000148",
                NomeResponsavel = "Nome Responsável",
                EmailResponsavel = "emailresponsavel@gmail.com",
                RazaoSocial = "RazaoSocial LTDA",
                NomeFantasia = "Nome Fantasia",
                Endereco = new Endereco(),
                Status = Status.Completo
            };

            // Act
            var result = EmailBuilder.BuildEmailBloqueado(empresa);

            // Assert
            Assert.Equal(empresa.EmailResponsavel, result.To.First());
            Assert.Equal(EmailTemplate.EMPRESABLOQUEADASUBJECT, result.Subject);
            Assert.Equal(NormalizeHTML(emailBloqueadoEsperado), NormalizeHTML(result.Content));
        }

        private string NormalizeHTML(string text) =>
            Regex.Replace(text, @"\s+", " ").Trim();

        private string emailCompletoEsperado = 
            @"<!DOCTYPE html>
            <html>
                <head>
                    <meta charset='UTF-8'>
                </head>

                <body style='margin:0; padding:0; background-color:#f4f4f4; font-family:Arial, 
                        sans-serif; font-weight:normal'>

                    <table width='100%' bgcolor='#f4f4f4' cellpadding='0' cellspacing='0'>
                        <tr>
                            <td align='center'>
                                <table width='600' bgcolor='#ffffff' cellpadding='20' cellspacing='0'
                                        style='border-radius:8px; box-shadow:0 0 10px rgba(0,0,0,0.1);'>

                                    <tr>
                                        <td align='center' style='background-color:#2c3e50; color:white;'>
                                            <div><h1>Olá, Nome Responsável!</h1></div>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td style='color:#333333; font-size:18px; line-height:1.5; text-align:justify;'>
                                            <div>
                                                <p>
						                            Gostaríamos de informar que seu cadastro foi realizado
                                                    com êxito e sua empresa de <strong>CNPJ 14.983.872/0001-48</strong> já consta em
                                                    nosso sistema!
                                                </p>                                    
                                                <p style='font-weight:bold'>
                                                    Todas as funcionalidades foram liberadas para utilização.
                                                </p>                                    
                                                <p style='font-size:14px;'>
                                                    Agradecemos a preferência em utilizar os nossos serviços!
                                                </p>
                                            </div>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td align='left'>
                                            <table cellpadding='0' cellspacing='0'>
                                                <tr>
                                                    <td>
                                                        <img src='https://seusite.com/logo-taxapi.png' 
                                                                width='24' height='24' style='display:block;'>
                                                    </td>
                                                    <td style='padding-left:8px; font-size:12px;'>
                                                        © 2026 TaxAPI. All rights reserved.
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td align='right' style='font-size:10px; 
                                            font-style:italic; padding-top:6px;'>
                                            01/01/2026 10:00:00
                                        </td>
                                    </tr>

                                </table>
                            </td>
                        </tr>
                    </table>
                </body>
            </html>";

        private string emailParcialEsperado =
            @"<!DOCTYPE html>
            <html>
                <head>
                    <meta charset='UTF-8'>
                </head>

                <body style='margin:0; padding:0; background-color:#f4f4f4; font-family:Arial, 
                        sans-serif; font-weight:normal'>

                    <table width='100%' bgcolor='#f4f4f4' cellpadding='0' cellspacing='0'>
                        <tr>
                            <td align='center'>
                                <table width='600' bgcolor='#ffffff' cellpadding='20' cellspacing='0'
                                        style='border-radius:8px; box-shadow:0 0 10px rgba(0,0,0,0.1);'>

                                    <tr>
                                        <td align='center' style='background-color:#2c3e50; color:white;'>
                                            <div><h1>Olá, Nome Responsável!</h1></div>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td style='color:#333333; font-size:18px; line-height:1.5; text-align:justify;'>
                                            <div>
                                                <p>
                                                    Gostaríamos de informar que seu cadastro foi realizado 
                                                    com êxito e sua empresa de <strong>CNPJ 14.983.872/0001-48</strong> 
                                                    já consta em nosso sistema!
                                                </p>
                                                <p>
                                                    Porém, <strong>detectamos a ausência de alguns dados</strong>. 
                                                    A não atualização dos dados nas próximas utilizações do serviço 
                                                    poderá acarretar em bloqueio da conta.
                                                </p>
                                                <p style='font-size:14px;'>
                                                    A atualização dos dados pode ser realizada pelo mesmo local 
                                                    de cadastro e emissão de novas notas.
                                                </p>
                                                <p style='font-size:14px;'>
                                                    Agradecemos a preferência em utilizar os nossos serviços!
                                                </p>
                                            </div>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td align='left'>
                                            <table cellpadding='0' cellspacing='0'>
                                                <tr>
                                                    <td>
                                                        <img src='https://seusite.com/logo-taxapi.png' 
                                                                width='24' height='24' style='display:block;'>
                                                    </td>
                                                    <td style='padding-left:8px; font-size:12px;'>
                                                        © 2026 TaxAPI. All rights reserved.
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td align='right' style='font-size:10px; 
                                            font-style:italic; padding-top:6px;'>
                                            01/01/2026 10:00:00
                                        </td>
                                    </tr>

                                </table>
                            </td>
                        </tr>
                    </table>
                </body>
            </html>";

        private string emailNotificadoEsperado =
            @"<!DOCTYPE html>
            <html>
                <head>
                    <meta charset='UTF-8'>
                </head>

                <body style='margin:0; padding:0; background-color:#f4f4f4; font-family:Arial, 
                        sans-serif; font-weight:normal'>

                    <table width='100%' bgcolor='#f4f4f4' cellpadding='0' cellspacing='0'>
                        <tr>
                            <td align='center'>
                                <table width='600' bgcolor='#ffffff' cellpadding='20' cellspacing='0'
                                        style='border-radius:8px; box-shadow:0 0 10px rgba(0,0,0,0.1);'>

                                    <tr>
                                        <td align='center' style='background-color:#2c3e50; color:white;'>
                                            <div><h1>Olá, Nome Responsável!</h1></div>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td style='color:#333333; font-size:18px; line-height:1.5; text-align:justify;'>
                                            <div>
                                                <p>
							                        Gostaríamos de informar que a atualização dos dados 
                                                    da empresa de <strong>CNPJ 14.983.872/0001-48</strong> ainda não foi realizada!
                                                </p>
                                                <p>
                                                    Informamos também o eventual <strong>bloqueio da empresa</strong> 
                                                    na próxima utilização do serviço de cálculo de impostos.
                                                </p>
                                                <p style='font-size:14px;'>
							                        O bloqueio da empresa impede a utilização de quaisquer
                                                    serviços ofertados pelo nosso sistema.
                                                </p>
                                            </div>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td align='left'>
                                            <table cellpadding='0' cellspacing='0'>
                                                <tr>
                                                    <td>
                                                        <img src='https://seusite.com/logo-taxapi.png' 
                                                                width='24' height='24' style='display:block;'>
                                                    </td>
                                                    <td style='padding-left:8px; font-size:12px;'>
                                                        © 2026 TaxAPI. All rights reserved.
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td align='right' style='font-size:10px; 
                                            font-style:italic; padding-top:6px;'>
                                            01/01/2026 10:00:00
                                        </td>
                                    </tr>

                                </table>
                            </td>
                        </tr>
                    </table>
                </body>
            </html>";

        private string emailBloqueadoEsperado =
            @"<!DOCTYPE html>
            <html>
                <head>
                    <meta charset='UTF-8'>
                </head>

                <body style='margin:0; padding:0; background-color:#f4f4f4; font-family:Arial, 
                        sans-serif; font-weight:normal'>

                    <table width='100%' bgcolor='#f4f4f4' cellpadding='0' cellspacing='0'>
                        <tr>
                            <td align='center'>
                                <table width='600' bgcolor='#ffffff' cellpadding='20' cellspacing='0'
                                        style='border-radius:8px; box-shadow:0 0 10px rgba(0,0,0,0.1);'>

                                    <tr>
                                        <td align='center' style='background-color:#2c3e50; color:white;'>
                                            <div><h1>Olá, Nome Responsável!</h1></div>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td style='color:#333333; font-size:18px; line-height:1.5; text-align:justify;'>
                                            <div>
                                                <p>
							                        Gostaríamos de informar que sua empresa de <strong>CNPJ 14.983.872/0001-48
                                                    </strong> foi bloqueada pela pendência de dados cadastrais!
                                                </p>
                                                <p>
                                                    Com o bloqueio, não é possível utilizar nossos serviços 
                                                    até atualização dos dados da empresa.
                                                </p>
                                                <p style='font-size:14px;'>
							                        A atualização dos dados pode ser realizada pelo mesmo 
                                                    local de cadastro e emissão de novas notas.
                                                </p>
                                            </div>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td align='left'>
                                            <table cellpadding='0' cellspacing='0'>
                                                <tr>
                                                    <td>
                                                        <img src='https://seusite.com/logo-taxapi.png' 
                                                                width='24' height='24' style='display:block;'>
                                                    </td>
                                                    <td style='padding-left:8px; font-size:12px;'>
                                                        © 2026 TaxAPI. All rights reserved.
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td align='right' style='font-size:10px; 
                                            font-style:italic; padding-top:6px;'>
                                            01/01/2026 10:00:00
                                        </td>
                                    </tr>

                                </table>
                            </td>
                        </tr>
                    </table>
                </body>
            </html>";
    }
}
