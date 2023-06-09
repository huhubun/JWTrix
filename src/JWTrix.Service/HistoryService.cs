using JWTrix.Data.Abstractions;
using JWTrix.Data.Entities;
using JWTrix.Data.Filters;
using JWTrix.Service.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTrix.Service;

public class HistoryService : IHistoryService
{
    private readonly IHistoryRepository _historyRepository;

    public HistoryService(
        IHistoryRepository historyRepository)
    {
        _historyRepository = historyRepository;
    }

    public async Task<long> GetCountAsync(Dictionary<FilterKey, object> filters)
    {
        return await _historyRepository.GetCountAsync(filters);
    }

    public async Task<List<History>> GetByPageAsync(int skip, int take)
    {
        return await _historyRepository.GetByPageAsync(skip, take);
    }

    public async Task<History?> GetByIdAsync(long id)
    {
        return await _historyRepository.GetByIdAsync(id);
    }

    public async Task<int> AddAsync(History history)
    {
        return await _historyRepository.InsertAsync(history);
    }

    public async Task<int> DeleteByIdAsync(long id)
    {
        return await _historyRepository.DeleteByIdAsync(id);
    }
}
