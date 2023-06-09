namespace JWTrix.Data.Sorters
{
    public static class SortListExtension
    {
        public static string? ToSql(this List<SortKey>? sorts, bool includeKeyword = true)
        {
            if (sorts == null)
            {
                return null;
            }

            var sql = String.Empty;

            if (includeKeyword)
            {
                sql += " ORDER BY ";
            }

            sql += String.Join(", ", sorts.Select(s => s.ToSql()));

            return sql;
        }
    }
}
