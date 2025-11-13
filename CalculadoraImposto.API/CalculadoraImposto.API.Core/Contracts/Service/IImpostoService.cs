using CalculadoraImposto.API.Core.Entities;

namespace CalculadoraImposto.API.Core.Contracts.Service
{
    public interface IImpostoService
    {
        Task<Imposto> CalcularImposto(NotaFiscal notaFiscal, Empresa empresa);

        Task<Imposto> ObterImpostoPorNF(int numeroNF, int serieNF);

        Task<Imposto> ObterImpostoPorCodigo(int codigo);

        Task<Imposto> ObterImpostoMaisRecente();

        Task<Imposto> ObterImpostoPorMes(DateTime? mes);

        Task<IEnumerable<Imposto>> ObterImpostosPorEmpresa(string cnpj);

        Task<IEnumerable<Imposto>> ObterImpostosPorSemestre(DateTime? semestre);
    }
}
