using RegistroNF.Core.Entities;

namespace RegistroNF.Architecture.Repositories
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

        protected IQueryable<T> Get() =>
            _context.Set<T>().AsQueryable();
    }
}
