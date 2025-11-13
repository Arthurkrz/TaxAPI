using CalculadoraImposto.API.Core.Entities;

namespace CalculadoraImposto.API.Core.Contracts.Repository
{
    public interface IImpostoRepository : IBaseRepository<Imposto>
    {
        Task<Imposto> ObterImpostoPorNF(int numeroNF, int serieNF);

        Task<Imposto> ObterImpostoPorCodigo(int codigo);

        Task<Imposto> ObterImpostoMaisRecente();

        Task<Imposto> ObterImpostoPorMes(DateTime? mes);

        Task<IEnumerable<Imposto>> ObterImpostosPorEmpresa(string cnpj);

        Task<IEnumerable<Imposto>> ObterImpostosPorSemestre(DateTime? semestre);
    }
}
