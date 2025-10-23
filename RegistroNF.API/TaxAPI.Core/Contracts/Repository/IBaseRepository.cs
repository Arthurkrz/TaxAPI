using RegistroNF.Core.Entities;

namespace RegistroNF.Architecture.Repositories
{
    public interface IBaseRepository<T> where T : Entity
    {
        T Create(T entity);
    }
}
