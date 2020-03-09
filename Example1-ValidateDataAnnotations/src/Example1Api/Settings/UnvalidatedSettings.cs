using Example1Api.Attributes;

namespace Example1Api.Settings
{
    [SettingsSectionName("Unvalidated")]
    public class UnvalidatedSettings
    {
        public string ParameterA { get; set; }
        public string ParameterB { get; set; }
    }
}