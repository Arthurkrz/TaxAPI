using CalculadoraImposto.API.Core.Entities;
using FluentValidation;

namespace CalculadoraImposto.API.Core.Validators
{
    public class EmpresaValidator : AbstractValidator<Empresa>
    {
        public EmpresaValidator()
        {
            this.RuleFor(e => e.CNPJ)
                .NotEmpty().WithMessage("O CNPJ é obrigatório")
                .Length(14).WithMessage("O CNPJ deve conter 14 caracteres");

            this.RuleFor(e => e.RazaoSocial)
                .NotEmpty().WithMessage("A razão social é obrigatória")
                .MaximumLength(100).WithMessage("A razão social deve conter no máximo 100 caracteres");

            this.RuleFor(e => e.NomeResponsavel)
                .NotEmpty().WithMessage("O nome do responsável é obrigatório")
                .Must(EhNomeValido)
                .WithMessage("Nome deve conter nome e sobrenome separados por um espaço com no mínimo 3 caracteres cada");

            this.RuleFor(e => e.EmailResponsavel)
                .NotEmpty().WithMessage("O email do responsável é obrigatório")
                .EmailAddress().WithMessage("O email do responsável deve ser um endereço de email válido");

            this.RuleFor(e => e.NotasFiscais)
                .NotEmpty().WithMessage("A empresa deve possuir ao menos uma nota fiscal");
        }

        private bool EhNomeValido(string nome)
        {
            var partes = nome.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            return partes.Length >= 2 && partes.All(p => p.Length >= 3);
        }
    }
}
