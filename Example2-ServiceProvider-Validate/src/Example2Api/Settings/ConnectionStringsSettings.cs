using Example2Api.Attributes;
using Example2Api.Interfaces;

namespace Example2Api.Options
{
    [SettingsSectionName("ConnectionStrings")]
    public class ConnectionStringsSettings : ICanValidate
    {
        public string Connection1 { get; set; }
        
        public bool IsValid()
        {
            if (string.IsNullOrEmpty(Connection1)) return false;
            
            return true;
        }
    }
}