using Example2Api.Attributes;
using Example2Api.Interfaces;

namespace Example2Api.Options
{
    [ConfigurationSectionName("Database")]
    public class DatabaseOptions : ICanValidate
    {
        public DatabaseSchemaNames SchemaNames { get; set; } = new DatabaseSchemaNames();
        
        public string Comment1 { get; set; }
        public string DatabaseType { get; set; }
        public string Comment2 { get; set; }
        
        public class DatabaseSchemaNames
        {
            public string Schema1 { get; set; }
        }

        public bool IsValid()
        {
            if (string.IsNullOrEmpty(Comment1)) return false;
            if (string.IsNullOrEmpty(DatabaseType)) return false;
            if (string.IsNullOrEmpty(Comment2)) return false;

            if (string.IsNullOrEmpty(SchemaNames?.Schema1)) return false;

            return true;
        }
    }
}