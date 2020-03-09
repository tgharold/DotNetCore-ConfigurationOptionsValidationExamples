using Example3Api.Attributes;

namespace Example3Api.Settings
{
    [SettingsSectionName("UnmonitoredButValidated")]
    public class UnmonitoredButValidatedSettings
    {
        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
    }
}