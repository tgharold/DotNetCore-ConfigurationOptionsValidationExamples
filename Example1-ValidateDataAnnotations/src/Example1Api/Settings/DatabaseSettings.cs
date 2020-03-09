using System.ComponentModel.DataAnnotations;
using Example1Api.Attributes;

namespace Example1Api.Settings
{
    [SettingsSectionName("Database")]
    public class DatabaseSettings
    {
        public DatabaseSchemaNames SchemaNames { get; set; } = new DatabaseSchemaNames();
        
        [Required]
        public string Comment1 { get; set; }
        
        [IsValidDatabaseType]
        public string DatabaseType { get; set; }
        
        [Required]
        public string Comment2 { get; set; }
        
        public class DatabaseSchemaNames
        {
            //NOTE: DataAnnotation validation does not validate sub-objects out of the box.  Custom attributes/code required. 
            
            [Required]
            public string Schema1 { get; set; }
        }
    }
}