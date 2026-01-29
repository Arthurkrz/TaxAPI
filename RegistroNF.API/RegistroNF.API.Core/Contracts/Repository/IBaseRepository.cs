using RegistroNF.API.Core.Entities;

namespace RegistroNF.API.Core.Contracts.Repository
{
    public interface IBaseRepository<T> where T : Entity
    {
        T Create(T entity);

        IQueryable<T> Get();
    }
}
