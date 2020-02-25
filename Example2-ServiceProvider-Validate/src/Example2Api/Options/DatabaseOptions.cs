using Example2Api.Attributes;
using Example2Api.Interfaces;

namespace Example2Api.Options
{
    [ConfigurationSectionName("Database")]
    public class DatabaseOptions : ICanValidate
    {
        public DatabaseSchemaNames SchemaNames { get; set; } = new DatabaseSchemaNames(); 
        public string DatabaseType { get; set; }
        
        public class DatabaseSchemaNames
        {
            public string Schema1 { get; set; }
        }

        public bool IsValid()
        {
            if (string.IsNullOrEmpty(DatabaseType)) return false;
            if (string.IsNullOrEmpty(SchemaNames?.Schema1)) return false;

            return true;
        }
    }
}