using FluentValidation;
using RegistroNF.API.Core.Common;
using RegistroNF.API.Core.Entities;

namespace RegistroNF.API.Core.Validators
{
    public class NotaFiscalValidator : AbstractValidator<NotaFiscal>
    {
        public NotaFiscalValidator()
        {
            this.RuleFor(nf => nf.Numero)
                .GreaterThan(0)
                .WithMessage(LogMessages.NUMEROMAIORQUEZERO);

            this.RuleFor(nf => nf.Serie)
                .GreaterThan(0)
                .WithMessage(LogMessages.SERIEMAIORQUEZERO);

            this.RuleFor(nf => nf.DataEmissao)
                .LessThanOrEqualTo(DateTime.Now)
                .WithMessage(LogMessages.DATAEMISSAONAOFUTURA);

            this.RuleFor(nf => nf.ValorBrutoProdutos)
                .GreaterThan(0)
                .WithMessage(LogMessages.VALORBRUTOMAIORQUEZERO);

            this.RuleFor(nf => nf.ValorTotalNota)
                .Must((nf, valorTotal) => Math.Abs(valorTotal - (nf.ValorBrutoProdutos + (nf.ValorBrutoProdutos * (nf.ValorICMS / 100)))) < 0.01)
                .WithMessage(LogMessages.VALORTOTALINVALIDO);

            this.RuleFor(nf => nf.Empresa)
                .NotNull()
                .WithMessage(LogMessages.EMPRESANAOINFORMADA);
        }
    }
}
