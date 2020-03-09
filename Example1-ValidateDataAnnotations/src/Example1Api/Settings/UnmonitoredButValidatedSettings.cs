using System.ComponentModel.DataAnnotations;
using Example1Api.Attributes;

namespace Example1Api.Settings
{
    [SettingsSectionName("UnmonitoredButValidated")]
    public class UnmonitoredButValidatedSettings
    {
        [Required]
        public string OptionA { get; set; }
        
        [Required]
        public string OptionB { get; set; }
        
        [Required]
        public string OptionC { get; set; }
    }
}