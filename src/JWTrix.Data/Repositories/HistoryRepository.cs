using JWTrix.Data.Abstractions;
using JWTrix.Data.Entities;
using JWTrix.Data.Filters;
using JWTrix.Data.Sorters;
using Microsoft.Data.Sqlite;

namespace JWTrix.Data.Repositories
{
    public class HistoryRepository : SqliteRepositoryBase<History>, IHistoryRepository
    {
        protected override string TableName => "History";

        public override async Task<int> CreateTableAsync()
        {
            var sql = $"""
                CREATE TABLE IF NOT EXISTS {TableName} (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    DecodeTime TEXT,
                    Content TEXT,
                    Header TEXT,
                    Payload TEXT
                )
                """;

            using var connection = await OpenConnectionAsync();
            using var command = new SqliteCommand(sql, connection);

            return await command.ExecuteNonQueryAsync();
        }

        public override async Task<long> GetCountAsync(Dictionary<FilterKey, object>? filters = null)
        {
            var sql = $"SELECT COUNT(Id) FROM {TableName} WHERE 1 = 1";
            sql = ApplyFilterSql(sql, filters);

            using var connection = await OpenConnectionAsync();
            using var command = new SqliteCommand(sql, connection);
            ApplyFilterCommand(command, filters);

            var result = await command.ExecuteScalarAsync();
            return (long?)result ?? default;
        }

        public override async Task<List<History>> GetAllAsync(List<SortKey>? sorts = null)
        {
            var result = new List<History>();

            var sql = $"""
                SELECT Id, DecodeTime, Content, Header, Payload
                FROM {TableName}
                {sorts.ToSql()}
                """;

            using var connection = await OpenConnectionAsync();
            using var command = new SqliteCommand(sql, connection);
            using var reader = await command.ExecuteReaderAsync();

            while (reader.Read())
            {
                result.Add(GetEntity(reader));
            }

            return result;
        }

        public override async Task<List<History>> GetByPageAsync(int skip, int take, List<SortKey>? sorts = null)
        {
            var result = new List<History>();

            sorts ??= new()
            {
                new("DecodeTime", OrderBy.Descending)
            };

            var sql = $"""
                SELECT Id, DecodeTime, Content, Header, Payload 
                FROM {TableName} 
                {sorts.ToSql()}
                LIMIT @Take OFFSET @Skip
                """;

            using var connection = await OpenConnectionAsync();
            using var command = new SqliteCommand(sql, connection);

            command.Parameters.AddWithValue("@Take", take);
            command.Parameters.AddWithValue("@Skip", skip);

            using var reader = await command.ExecuteReaderAsync();

            while (reader.Read())
            {
                result.Add(GetEntity(reader));
            }

            return result;
        }

        public override async Task<History?> GetByIdAsync(long id)
        {
            var sql = $"""
                SELECT Id, DecodeTime, Content, Header, Payload
                FROM {TableName}
                WHERE Id = @Id
                LIMIT 1
                """;

            using var connection = await OpenConnectionAsync();
            using var command = new SqliteCommand(sql, connection);
            command.Parameters.AddWithValue("@Id", id);

            using var reader = await command.ExecuteReaderAsync();

            while (reader.Read())
            {
                return GetEntity(reader);
            }

            return null;
        }

        public override async Task<int> InsertAsync(History entity)
        {
            var insertSql = $"""
                INSERT INTO {TableName} (DecodeTime, Content, Header, Payload)
                VALUES (@DecodeTime, @Content, @Header, @Payload)
                """;

            using var connection = await OpenConnectionAsync();
            using var insertCommand = new SqliteCommand(insertSql, connection);

            insertCommand.Parameters.AddWithValue("@DecodeTime", ConvertToTimeString(entity.DecodeTime));
            insertCommand.Parameters.AddWithValue("@Content", entity.Content);
            insertCommand.Parameters.AddWithValue("@Header", entity.Header);
            insertCommand.Parameters.AddWithValue("@Payload", entity.Payload);

            var insertResult = await insertCommand.ExecuteNonQueryAsync();

            string selectIdSql = "SELECT LAST_INSERT_ROWID()";
            using var selectIdCommand = new SqliteCommand(selectIdSql, connection);

            entity.Id = (long?)await selectIdCommand.ExecuteScalarAsync() ?? default;

            return insertResult;
        }

        public override async Task<int> DeleteByIdAsync(long id)
        {
            var sql = $"""
                DELETE FROM {TableName}
                WHERE Id = @Id
                """;

            using var connection = await OpenConnectionAsync();
            using var command = new SqliteCommand(sql, connection);
            command.Parameters.AddWithValue("@Id", id);

            return await command.ExecuteNonQueryAsync();
        }

        private static History GetEntity(SqliteDataReader reader) => new()
        {
            Id = reader.GetInt32(0),
            DecodeTime = ConvertFromTimeString(reader.GetString(1)),
            Content = reader.GetString(2),
            Header = reader.GetString(3),
            Payload = reader.GetString(4)
        };

        private static string ApplyFilterSql(string sql, Dictionary<FilterKey, object>? filters)
        {
            if (filters != null)
            {
                foreach (var filter in filters)
                {
                    sql += filter.Key.ToSql();
                }
            }

            return sql;
        }

        private static void ApplyFilterCommand(SqliteCommand command, Dictionary<FilterKey, object>? filters)
        {
            if (filters != null)
            {
                foreach (var filter in filters)
                {
                    command.Parameters.AddWithValue(filter.Key.ActualParameterName, filter.Value);
                }
            }
        }
    }
}
