using System;
using Example2Api.Attributes;
using Example2Api.Interfaces;

namespace Example2Api.Options
{
    [ConfigurationSectionName("ConnectionStrings")]
    public class ConnectionStringsOptions : ICanValidate
    {
        public string Connection1 { get; set; }
        
        public bool IsValid()
        {
            if (string.IsNullOrEmpty(Connection1)) return false;
            
            return true;
        }
    }
}