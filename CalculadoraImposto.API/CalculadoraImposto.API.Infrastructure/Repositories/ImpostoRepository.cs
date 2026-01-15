using CalculadoraImposto.API.Core.Contracts.Repository;
using CalculadoraImposto.API.Core.Entities;

namespace CalculadoraImposto.API.Infrastructure.Repositories
{
    public class ImpostoRepository : BaseRepository<Imposto>, IImpostoRepository
    {
        public ImpostoRepository(Context context) : base(context) { }
    }
}
