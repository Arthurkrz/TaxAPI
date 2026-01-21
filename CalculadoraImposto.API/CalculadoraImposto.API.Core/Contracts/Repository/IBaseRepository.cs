namespace CalculadoraImposto.API.Core.Contracts.Repository
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> CreateAsync(T entity);

        IQueryable<T> Get();
    }
}
