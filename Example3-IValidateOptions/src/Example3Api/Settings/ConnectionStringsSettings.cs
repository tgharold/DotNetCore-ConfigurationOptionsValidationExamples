using Example3Api.Attributes;

namespace Example3Api.Settings
{
    [ConfigurationSectionName("ConnectionStrings")]
    public class ConnectionStringsSettings
    {
        public string Connection1 { get; set; }
        public string Connection2 { get; set; }
        public string Connection3 { get; set; }
    }
}