using FluentValidation;
using RegistroNF.Core.Contracts.Repository;
using TaxAPI.Core.Entities;

namespace RegistroNF.Core.Validators
{
    public class NotaFiscalValidator : AbstractValidator<NotaFiscal>
    {
        private readonly INotaFiscalRepository _notaFiscalRepository;

        public NotaFiscalValidator(INotaFiscalRepository notaFiscalRepository)
        {
            _notaFiscalRepository = notaFiscalRepository;

            this.RuleFor(nf => nf.Numero)
                .GreaterThan(0)
                .WithMessage("O número da nota fiscal deve ser maior que zero.");

            this.RuleFor(nf => nf.Numero)
                .Must((nf, numero) => GetSerieNF(nf.Serie).Any(x => x.Numero == numero))
                .WithMessage("Já existe uma nota fiscal com este número na mesma série.");

            this.RuleFor(nf => nf.Numero).Les

            this.RuleFor(nf => nf.Serie)
                .GreaterThan(0)
                .WithMessage("A série da nota fiscal deve ser maior que zero.");

            this.RuleFor(nf => nf.DataEmissao)
                .LessThanOrEqualTo(DateTime.Now)
                .WithMessage("A data de emissão não pode ser futura.");

            this.RuleFor(nf => nf.ValorBrutoProdutos)
                .GreaterThan(0)
                .WithMessage("O valor bruto dos produtos deve ser maior que zero.");

            this.RuleFor(nf => nf.ValorICMS)
                .GreaterThanOrEqualTo(0)
                .WithMessage("O valor do ICMS não pode ser negativo.")
                .LessThanOrEqualTo(nf => nf.ValorBrutoProdutos)
                .WithMessage("O valor do ICMS não pode ser maior que o valor bruto dos produtos.");

            this.RuleFor(nf => nf.ValorTotalNota)
                .NotNull()
                .WithMessage("O valor total da nota fiscal deve ser informado.")
                .Must((nf, valorTotal) => Math.Abs(valorTotal - (nf.ValorBrutoProdutos + nf.ValorICMS)) < 0.01)
                .WithMessage("O valor total da nota fiscal deve ser igual ao valor bruto dos produtos mais o valor do ICMS.");

            this.RuleFor(nf => nf.Empresa)
                .NotNull()
                .WithMessage("A empresa emissora da nota fiscal deve ser informada.");
        }

        private List<NotaFiscal> GetSerieNF(int serie) =>
            _notaFiscalRepository.GetSerieNF(serie);
    }
}
