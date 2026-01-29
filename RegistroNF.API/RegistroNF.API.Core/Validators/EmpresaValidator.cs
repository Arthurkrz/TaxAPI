using FluentValidation;
using RegistroNF.API.Core.Entities;

namespace RegistroNF.API.Core.Validators
{
    public class EmpresaValidator : AbstractValidator<Empresa>
    {
        public EmpresaValidator()
        {
            this.RuleFor(e => e.CNPJ)
                .Length(14)
                .WithMessage("O CNPJ deve conter 14 dígitos")
                .When(e => !string.IsNullOrWhiteSpace(e.CNPJ))
                .NotEmpty()
                .WithMessage("O CNPJ da empresa deve ser informado");

            this.RuleFor(e => e.NomeResponsavel)
                .Length(2, 100)
                .WithMessage("O nome do responsável deve ter entre 2 e 100 caracteres")
                .When(e => !string.IsNullOrWhiteSpace(e.NomeResponsavel))
                .NotEmpty()
                .WithMessage("O nome do responsável deve ser informado");

            this.RuleFor(e => e.EmailResponsavel)
                .EmailAddress()
                .WithMessage("O email do responsável deve ser um endereço de email válido")
                .When(e => !string.IsNullOrWhiteSpace(e.EmailResponsavel))
                .NotEmpty()
                .WithMessage("O email do responsável deve ser informado");
        }
    }
}