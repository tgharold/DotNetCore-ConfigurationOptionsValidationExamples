using System;
using Example1Api.Attributes;
using Example1Api.Interfaces;

namespace Example1Api.Options
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