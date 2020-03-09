using Example3Api.Attributes;

namespace Example3Api.Settings
{
    [SettingsSectionName("Unvalidated")]
    public class UnvalidatedSettings
    {
        public string ParameterA { get; set; }
        public string ParameterB { get; set; }
    }
}