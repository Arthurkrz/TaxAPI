using CalculadoraImposto.API.Core.Contracts.Repository;
using CalculadoraImposto.API.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CalculadoraImposto.API.Infrastructure.Repositories
{
    public class EmpresaRepository : BaseRepository<Empresa>, IEmpresaRepository
    {
        public EmpresaRepository(Context context) : base(context) { }

        public async Task<Empresa?> GetAsync(string cnpj) =>
            await this.Get().FirstOrDefaultAsync(e => e.CNPJ == cnpj);
    }
}
