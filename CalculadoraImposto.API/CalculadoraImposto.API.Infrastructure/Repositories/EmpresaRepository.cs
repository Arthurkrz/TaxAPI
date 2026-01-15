using CalculadoraImposto.API.Core.Contracts.Repository;
using CalculadoraImposto.API.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CalculadoraImposto.API.Infrastructure.Repositories
{
    public class EmpresaRepository : BaseRepository<Empresa>, IEmpresaRepository
    {
        private readonly Context _context;

        public EmpresaRepository(Context context) : base(context) { }

        public async Task<Empresa?> GetAsync(string cnpj) =>
            await _context.Empresas.FirstOrDefaultAsync(e => e.CNPJ == cnpj);

        public async Task RegistraNFsAsync(IList<NotaFiscal> notasFiscais) =>
            await _context.NotasFiscais.AddRangeAsync(notasFiscais);
    }
}
