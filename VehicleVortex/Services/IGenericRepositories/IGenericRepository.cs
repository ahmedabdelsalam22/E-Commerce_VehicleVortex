using System.Linq.Expressions;

namespace VehicleVortex.Services.IGenericRepositories
{
    public interface IGenericRepository<T>
    {
        Task<List<T>> GetAll(bool tracked = true);
        Task<T> Get(Expression<Func<T, bool>>? filter = null, bool tracked = true);
        Task Create(T entity);
        Task Delete(T entity);
    }
}
