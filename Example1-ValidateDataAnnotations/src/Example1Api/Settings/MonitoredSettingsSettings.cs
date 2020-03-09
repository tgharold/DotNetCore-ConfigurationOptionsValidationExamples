using Example1Api.Attributes;

namespace Example1Api.Settings
{
    [SettingsSectionName("MonitoredSettings")]
    public class MonitoredSettingsSettings
    {
        public string MonitorA { get; set; }
        public string MonitorB { get; set; }
    }
}