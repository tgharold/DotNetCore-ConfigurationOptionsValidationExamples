using Example3ApiAutofac.Attributes;

namespace Example3ApiAutofac.Options
{
    [ConfigurationSectionName("Unvalidated")]
    public class UnvalidatedOptions
    {
        public string ParameterA { get; set; }
        public string ParameterB { get; set; }
    }
}