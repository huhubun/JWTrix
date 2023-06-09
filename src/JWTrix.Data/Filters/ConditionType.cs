namespace JWTrix.Data.Filters
{
    public enum ConditionType
    {
        None,

        [SqlTemplat("=")]
        Equals,

        [SqlTemplat("<>")]
        NotEquals,

        [SqlTemplat(">")]
        GreaterThan,

        [SqlTemplat("<=")]
        NotGreaterThan,

        [SqlTemplat("<")]
        LessThan,

        [SqlTemplat(">=")]
        NotLessThan,

        [SqlTemplat(">=")]
        GreaterThanOrEqual,

        [SqlTemplat("<")]
        NotGreaterThanOrEqual,

        [SqlTemplat("<=")]
        LessThanOrEqual,

        [SqlTemplat(">")]
        NotLessThanOrEqual,

        [SqlTemplat("{columnName} LIKE {parameterName}", IsCustomFormat = true)]
        Contains,

        [SqlTemplat("{columnName} NOT LIKE {parameterName}", IsCustomFormat = true)]
        NotContains
    }
}
