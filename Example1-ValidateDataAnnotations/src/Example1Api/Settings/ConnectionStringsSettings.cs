using System.ComponentModel.DataAnnotations;
using Example1Api.Attributes;

namespace Example1Api.Settings
{
    [SettingsSectionName("ConnectionStrings")]
    public class ConnectionStringsSettings
    {
        [Required]
        public string Connection1 { get; set; }
        
        [Required]
        public string Connection2 { get; set; }
    }
}