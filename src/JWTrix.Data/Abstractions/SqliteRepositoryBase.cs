using JWTrix.Data.Filters;
using JWTrix.Data.Sorters;
using Microsoft.Data.Sqlite;

namespace JWTrix.Data.Abstractions
{
    public abstract class SqliteRepositoryBase
    {
        private const string DATABASE_FILE_NAME = "jwtrix.db";
        private const string TIME_FORMAT = "yyyy-MM-dd HH:mm:ss.fffZ";

        public static string DatabaseFilePath => Path.Combine(FileSystem.AppDataDirectory, DATABASE_FILE_NAME);
        protected abstract string TableName { get; }

        protected async Task<SqliteConnection> OpenConnectionAsync()
        {
            var connection = new SqliteConnection($"Data Source={DatabaseFilePath}");
            await connection.OpenAsync();

            return connection;
        }

        protected static string ConvertToTimeString(DateTimeOffset dateTimeOffset)
        {
            return dateTimeOffset.ToString(TIME_FORMAT);
        }

        protected static DateTimeOffset ConvertFromTimeString(string timeString)
        {
            return DateTimeOffset.ParseExact(timeString, TIME_FORMAT, System.Globalization.DateTimeFormatInfo.InvariantInfo);
        }
    }

    public abstract class SqliteRepositoryBase<T> : SqliteRepositoryBase, IRepository<T> where T : EntityBase, new()
    {
        public abstract Task<int> CreateTableAsync();
        public abstract Task<long> GetCountAsync(Dictionary<FilterKey, object>? filters = null);
        public abstract Task<List<T>> GetAllAsync(List<SortKey>? sorts = null);
        public abstract Task<List<T>> GetByPageAsync(int skip, int take, List<SortKey>? sorts = null);
        public abstract Task<T?> GetByIdAsync(long id);
        public abstract Task<int> InsertAsync(T entity);
        public abstract Task<int> DeleteByIdAsync(long id);
    }
}
