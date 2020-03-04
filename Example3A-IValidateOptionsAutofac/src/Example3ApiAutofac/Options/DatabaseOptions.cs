using Example3ApiAutofac.Attributes;

namespace Example3ApiAutofac.Options
{
    [ConfigurationSectionName("Database")]
    public class DatabaseOptions
    {
        public DatabaseSchemaNames SchemaNames { get; set; } = new DatabaseSchemaNames();
        
        public string Comment1 { get; set; }
        public string DatabaseType { get; set; }
        public string Comment2 { get; set; }
        
        public class DatabaseSchemaNames
        {
            public string Schema1 { get; set; }
            public string Schema2 { get; set; }
        }
    }
}