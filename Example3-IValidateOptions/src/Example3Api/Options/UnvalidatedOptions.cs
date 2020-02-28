using Example3Api.Attributes;

namespace Example3Api.Options
{
    [ConfigurationSectionName("Unvalidated")]
    public class UnvalidatedOptions
    {
        public string ParameterA { get; set; }
        public string ParameterB { get; set; }
    }
}