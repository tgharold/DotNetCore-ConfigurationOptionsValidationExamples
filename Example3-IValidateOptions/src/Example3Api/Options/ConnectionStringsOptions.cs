using Example3Api.Attributes;

namespace Example3Api.Options
{
    [ConfigurationSectionName("ConnectionStrings")]
    public class ConnectionStringsOptions
    {
        public string Connection1 { get; set; }
        public string Connection2 { get; set; }
        public string Connection3 { get; set; }
    }
}