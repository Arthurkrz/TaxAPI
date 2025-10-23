using FluentValidation;

namespace RegistroNF.Core.Validators
{
    public class NotaFiscalValidator : AbstractValidator<NotaFiscal>
    {
        public NotaFiscalValidator()
        {
            this.RuleFor(nf => nf.Numero)
                .GreaterThan(0)
                .WithMessage("O número da nota fiscal deve ser maior que zero");

            this.RuleFor(nf => nf.Serie)
                .GreaterThan(0)
                .WithMessage("A série da nota fiscal deve ser maior que zero");

            this.RuleFor(nf => nf.DataEmissao)
                .LessThanOrEqualTo(DateTime.Now)
                .WithMessage("A data de emissão não pode ser futura");

            this.RuleFor(nf => nf.ValorBrutoProdutos)
                .GreaterThan(0)
                .WithMessage("O valor bruto dos produtos deve ser maior que zero");

            this.RuleFor(nf => nf.ValorTotalNota)
                .Must((nf, valorTotal) => Math.Abs(valorTotal - (nf.ValorBrutoProdutos + (nf.ValorBrutoProdutos * (nf.ValorICMS / 100)))) < 0.01)
                .WithMessage("O valor total da nota fiscal deve ser igual ao valor bruto dos produtos mais o valor do ICMS");

            this.RuleFor(nf => nf.Empresa)
                .NotNull()
                .WithMessage("A empresa emissora da nota fiscal deve ser informada");
        }
    }
}
