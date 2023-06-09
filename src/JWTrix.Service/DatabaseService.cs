using JWTrix.Data.Abstractions;
using JWTrix.Service.Abstractions;
using Microsoft.Extensions.Logging;

namespace JWTrix.Service;

public class DatabaseService : IDatabaseService
{
    private readonly ILogger<DatabaseService> _logger;
    private readonly IHistoryRepository _historyRepository;

    public DatabaseService(
        ILogger<DatabaseService> logger,
        IHistoryRepository historyRepository)
    {
        _logger = logger;
        _historyRepository = historyRepository;
    }

    public async Task InitDatabaseAsync()
    {
        if (!File.Exists(SqliteRepositoryBase.DatabaseFilePath))
        {
            var result = await _historyRepository.CreateTableAsync();

            _logger.LogInformation("Create [History] table: {result}", result);
        }
    }
}