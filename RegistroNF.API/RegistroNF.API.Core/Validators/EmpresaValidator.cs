using FluentValidation;
using RegistroNF.API.Core.Common;
using RegistroNF.API.Core.Entities;

namespace RegistroNF.API.Core.Validators
{
    public class EmpresaValidator : AbstractValidator<Empresa>
    {
        public EmpresaValidator()
        {
            this.RuleFor(e => e.CNPJ)
                .Length(14)
                .WithMessage(LogMessages.CNPJINVALIDO)
                .When(e => !string.IsNullOrWhiteSpace(e.CNPJ))
                .NotEmpty()
                .WithMessage(LogMessages.CNPJNAOINFORMADO);

            this.RuleFor(e => e.NomeResponsavel)
                .Length(2, 100)
                .WithMessage(LogMessages.NOMERESPONSAVELINVALIDO)
                .When(e => !string.IsNullOrWhiteSpace(e.NomeResponsavel))
                .NotEmpty()
                .WithMessage(LogMessages.NOMERESPONSAVELNAOINFORMADO);

            this.RuleFor(e => e.EmailResponsavel)
                .EmailAddress()
                .WithMessage(LogMessages.EMAILRESPONSAVELINVALIDO)
                .When(e => !string.IsNullOrWhiteSpace(e.EmailResponsavel))
                .NotEmpty()
                .WithMessage(LogMessages.EMAILRESPONSAVELNAOINFORMADO);
        }
    }
}