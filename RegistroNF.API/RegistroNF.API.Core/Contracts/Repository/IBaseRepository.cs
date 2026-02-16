using RegistroNF.API.Core.Entities;

namespace RegistroNF.API.Core.Contracts.Repository
{
    public interface IBaseRepository<T> where T : Entity
    {
        Task<T> CreateAsync(T entity);

        IQueryable<T> Get();

        Task UpdateAsync(T entity);
    }
}
