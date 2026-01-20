using CalculadoraImposto.API.Core.Contracts.Repository;
using CalculadoraImposto.API.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CalculadoraImposto.API.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : Entity
    {
        private readonly Context _context;

        public BaseRepository(Context context)
        {
            _context = context;
        }

        public async Task<T> CreateAsync(T entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        protected async Task<IQueryable<T>> GetAsync() =>
            await _context.Set<T>().ToListAsync();
    }
}
