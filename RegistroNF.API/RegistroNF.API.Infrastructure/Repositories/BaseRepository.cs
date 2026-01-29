using RegistroNF.API.Core.Contracts.Repository;
using RegistroNF.API.Core.Entities;

namespace RegistroNF.API.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : Entity
    {
        private readonly Context _context;

        public BaseRepository(Context context)
        {
            _context = context;
        }

        public T Create(T entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public IQueryable<T> Get() => _context.Set<T>();
    }
}
