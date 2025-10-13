using FluentValidation;
using RegistroNF.Core.Entities;

namespace RegistroNF.Core.Validators
{
    public class EmpresaValidator : AbstractValidator<Empresa>
    {
        public EmpresaValidator()
        {
            this.RuleFor(e => e.CNPJ)
                .NotNull()
                .WithMessage("O CNPJ da empresa deve ser informado.")
                .Length(14)
                .WithMessage("O CNPJ deve conter 14 dígitos.");

            this.RuleFor(e => e.NomeResponsavel)
                .NotNull()
                .WithMessage("O nome do responsável deve ser informado.")
                .NotEmpty()
                .WithMessage("O nome do responsável não pode ser vazio.")
                .Length(2, 100)
                .WithMessage("O nome do responsável deve ter entre 2 e 100 caracteres.");

            this.RuleFor(e => e.EmailResponsavel)
                .NotNull()
                .WithMessage("O email do responsável deve ser informado.")
                .NotEmpty()
                .WithMessage("O email do responsável não pode ser vazio.")
                .EmailAddress()
                .WithMessage("O email do responsável deve ser um endereço de email válido.");
        }
    }
}
