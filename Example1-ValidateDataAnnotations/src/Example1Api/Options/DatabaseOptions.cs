using System;
using Example1Api.Attributes;
using Example1Api.Interfaces;

namespace Example1Api.Options
{
    [ConfigurationSectionName("Database")]
    public class DatabaseOptions : ICanValidate
    {
        public DatabaseSchemaNames SchemaNames { get; set; } = new DatabaseSchemaNames(); 
        public string DatabaseType { get; set; }
        
        public class DatabaseSchemaNames
        {
            public string ZebraPillarEmerald { get; set; } = "zpe";
        }

        public bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}