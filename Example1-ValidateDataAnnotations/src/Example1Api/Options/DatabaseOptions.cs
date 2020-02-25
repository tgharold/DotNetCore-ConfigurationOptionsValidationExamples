using System.ComponentModel.DataAnnotations;
using Example1Api.Attributes;

namespace Example1Api.Options
{
    [ConfigurationSectionName("Database")]
    public class DatabaseOptions
    {
        public DatabaseSchemaNames SchemaNames { get; set; } = new DatabaseSchemaNames();
        
        [Required]
        public string Comment1 { get; set; }
        
        [Required]
        public string DatabaseType { get; set; }
        
        [Required]
        public string Comment2 { get; set; }
        
        public class DatabaseSchemaNames
        {
            [Required]
            public string Schema1 { get; set; }
        }
    }
}