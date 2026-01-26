using CalculadoraImposto.API.ScheduledJobs.DTOs;
using FluentValidation;

namespace CalculadoraImposto.API.Core.Validators
{
    public class EmpresaDTOValidator : AbstractValidator<EmpresaDTO>
    {
        public EmpresaDTOValidator()
        {
            this.RuleFor(e => e.CNPJ)
                .NotEmpty().WithMessage("O CNPJ é obrigatório");

            this.RuleFor(e => e.RazaoSocial)
                .NotEmpty().WithMessage("A razão social é obrigatória");

            this.RuleFor(e => e.NomeResponsavel)
                .NotEmpty().WithMessage("O nome do responsável é obrigatório");

            this.RuleFor(e => e.EmailResponsavel)
                .NotEmpty().WithMessage("O email do responsável é obrigatório");

            this.RuleFor(e => e.NotasFiscais)
                .NotEmpty().WithMessage("A empresa deve possuir ao menos uma nota fiscal");
        }
    }
}
