using System.ComponentModel.DataAnnotations;
using Example1Api.Attributes;

namespace Example1Api.Options
{
    [ConfigurationSectionName("Database")]
    public class DatabaseOptions
    {
        public DatabaseSchemaNames SchemaNames { get; set; } = new DatabaseSchemaNames();
        
        [Required]
        public string DatabaseType { get; set; }
        
        public class DatabaseSchemaNames
        {
            [Required]
            public string ZebraPillarEmerald { get; set; }
        }
    }
}