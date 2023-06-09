namespace JWTrix.Data.Sorters
{
    public class SortKey
    {
        public SortKey(string columnName) : this(columnName, OrderBy.Ascending)
        {
        }

        public SortKey(string columnName, OrderBy orderType)
        {
            ColumnName = columnName;
            OrderType = orderType;
        }

        public string ColumnName { get; set; }
        public OrderBy OrderType { get; set; }
        public string Order => OrderType switch
        {
            OrderBy.Ascending => "ASC",
            OrderBy.Descending => "DESC",
            _ => throw new NotSupportedException()
        };

        public string ToSql()
        {
            return $" {ColumnName} {Order}";
        }
    }
}
