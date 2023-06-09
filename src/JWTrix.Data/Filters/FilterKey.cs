using System.Reflection;

namespace JWTrix.Data.Filters
{
    public class FilterKey : IEquatable<FilterKey?>
    {
        public FilterKey(string columnName, ConditionType conditionType)
        {
            ColumnName = columnName;
            ConditionType = conditionType;
        }

        public string ColumnName { get; set; }
        public ConditionType ConditionType { get; set; }
        public string? ParameterName { get; set; }
        public string ActualParameterName => "@" + (String.IsNullOrEmpty(ParameterName) ? ColumnName : ParameterName);

        public override bool Equals(object? obj)
        {
            return Equals(obj as FilterKey);
        }

        public bool Equals(FilterKey? other)
        {
            return other is not null && ToSql() == other.ToSql();
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ToSql());
        }

        public string? ToSql()
        {
            var sqlTemplatAttribute = typeof(ConditionType).GetField(ConditionType.ToString())?.GetCustomAttribute<SqlTemplatAttribute>() ?? null;

            if (sqlTemplatAttribute == null)
            {
                return null;
            }

            string sql;

            if (sqlTemplatAttribute.IsCustomFormat)
            {
                sql = sqlTemplatAttribute.Template
                    .Replace("{columnName}", ColumnName)
                    .Replace("{parameterName}", ActualParameterName);
            }
            else
            {
                sql = $"{ColumnName} {sqlTemplatAttribute.Template} {ActualParameterName}";
            }

            return " AND " + sql;
        }
    }
}
