using RegistroNF.API.Core.Common;
using RegistroNF.API.Core.Entities;
using System.Text.RegularExpressions;

namespace RegistroNF.API.Services.Utilities
{
    public static class EmailBuilder
    {
        public static EmailMessage BuildEmailCompleto(Empresa empresa) =>
            new EmailMessage()
            {
                To = [empresa.EmailResponsavel],
                Subject = EmailTemplate.EMPRESANOVACOMPLETASUBJECT,
                Content = BuildContent(empresa, EmailTemplate.EMPRESANOVACOMPLETABODY)
            };

        public static EmailMessage BuildEmailParcial(Empresa empresa) =>
            new EmailMessage()
            {
                To = [empresa.EmailResponsavel],
                Subject = EmailTemplate.EMPRESANOVAPARCIALSUBJECT,
                Content = BuildContent(empresa, EmailTemplate.EMPRESANOVAPARCIALBODY),
            };

        public static EmailMessage BuildEmailNotificado(Empresa empresa) =>
            new EmailMessage()
            {
                To = [empresa.EmailResponsavel],
                Subject = EmailTemplate.EMPRESANOTIFICADASUBJECT,
                Content = BuildContent(empresa, EmailTemplate.EMPRESANOTIFICADABODY)
            };

        public static EmailMessage BuildEmailBloqueado(Empresa empresa) =>
            new EmailMessage()
            {
                To = [empresa.EmailResponsavel],
                Subject = EmailTemplate.EMPRESABLOQUEADASUBJECT,
                Content = BuildContent(empresa,EmailTemplate.EMPRESABLOQUEADABODY)
            };

        private static string BuildContent(Empresa empresa, string body)
        {
            var cnpj = Regex.Replace(empresa.CNPJ, 
                @"(\d{2})(\d{3})(\d{3})(\d{4})(\d{2})", "$1.$2.$3/$4-$5");

            var now = SystemTime.Now();

            var header = EmailTemplate.HEADER.Replace("{NAME}", empresa.NomeResponsavel);
            body = body.Replace("{CNPJ}", cnpj);

            var footer = EmailTemplate.FOOTER
                .Replace("{YEAR}", now.Year.ToString())
                .Replace("{dd/MM/yyyy}", now.ToString("dd/MM/yyyy"))
                .Replace("{HH:mm:ss}", now.ToString("HH:mm:ss"));

            var content = EmailTemplate.BACKBONE
                .Replace("{BODY}", body)
                .Replace("{HEADER}", header)
                .Replace("{FOOTER}", footer);

            return content;
        }
    }
}
