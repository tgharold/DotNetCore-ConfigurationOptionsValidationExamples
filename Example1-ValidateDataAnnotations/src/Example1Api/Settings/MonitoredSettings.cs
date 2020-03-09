using System.ComponentModel.DataAnnotations;
using Example1Api.Attributes;

namespace Example1Api.Settings
{
    [SettingsSectionName("MonitoredSettings")]
    public class MonitoredSettings
    {
        [Required]
        public string MonitorA { get; set; }
        
        [Required]
        public string MonitorB { get; set; }
    }
}