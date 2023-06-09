namespace JWTrix.Data.Filters
{
    [AttributeUsage(AttributeTargets.Field)]
    internal class SqlTemplatAttribute : Attribute
    {
        public SqlTemplatAttribute(string template)
        {
            Template = template;
        }

        public string Template { get; private set; }
        public bool IsCustomFormat { get; set; }
    }
}
