using Example3Api.Attributes;

namespace Example3Api.Options
{
    [ConfigurationSectionName("MonitoredSettings")]
    public class MonitoredSettingsOptions
    {
        public string MonitorA { get; set; }
        public string MonitorB { get; set; }
    }
}