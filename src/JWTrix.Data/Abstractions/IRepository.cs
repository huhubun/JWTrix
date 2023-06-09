using JWTrix.Data.Filters;
using JWTrix.Data.Sorters;

namespace JWTrix.Data.Abstractions
{
    public interface IRepository<T> where T : EntityBase, new()
    {
        Task<int> CreateTableAsync();
        Task<long> GetCountAsync(Dictionary<FilterKey, object>? filters = null);
        Task<List<T>> GetAllAsync(List<SortKey>? sorts = null);
        Task<List<T>> GetByPageAsync(int skip, int take, List<SortKey>? sorts = null);
        Task<T?> GetByIdAsync(long id);
        Task<int> InsertAsync(T entity);
        Task<int> DeleteByIdAsync(long id);
    }
}
