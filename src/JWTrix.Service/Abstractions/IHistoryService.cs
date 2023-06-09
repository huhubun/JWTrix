using JWTrix.Data.Entities;
using JWTrix.Data.Filters;

namespace JWTrix.Service.Abstractions
{
    public interface IHistoryService
    {
        Task<long> GetCountAsync(Dictionary<FilterKey, object> filters);
        Task<List<History>> GetByPageAsync(int skip, int take);
        Task<History?> GetByIdAsync(long id);
        Task<int> AddAsync(History history);
        Task<int> DeleteByIdAsync(long id);
    }
}