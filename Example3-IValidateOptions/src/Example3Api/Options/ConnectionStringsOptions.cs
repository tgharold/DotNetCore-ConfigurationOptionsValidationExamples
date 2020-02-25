using Example3Api.Attributes;
using Example3Api.Interfaces;

namespace Example3Api.Options
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