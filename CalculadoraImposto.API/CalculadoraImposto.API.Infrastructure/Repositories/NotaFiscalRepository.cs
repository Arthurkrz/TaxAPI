using CalculadoraImposto.API.Core.Contracts.Repository;
using CalculadoraImposto.API.Core.Entities;

namespace CalculadoraImposto.API.Infrastructure.Repositories
{
    public class NotaFiscalRepository : BaseRepository<NotaFiscal>, INotaFiscalRepository
    {
        private readonly Context _context;

        public NotaFiscalRepository(Context context) : base(context) 
        { 
            _context = context; 
        }

        public async Task RegistraNFsAsync(IList<NotaFiscal> notasFiscais) =>
            await _context.NotasFiscais.AddRangeAsync(notasFiscais);
    }
}
