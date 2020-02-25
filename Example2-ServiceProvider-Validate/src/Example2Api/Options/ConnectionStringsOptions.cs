using System;
using Example2Api.Attributes;
using Example2Api.Interfaces;

namespace Example2Api.Options
{
    [ConfigurationSectionName("ConnectionStrings")]
    public class ConnectionStringsOptions : ICanValidate
    {
        public string ZebraPillarEmerald { get; set; }
        
        public bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}