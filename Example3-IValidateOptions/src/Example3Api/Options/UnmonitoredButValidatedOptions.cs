using Example3Api.Attributes;

namespace Example3Api.Options
{
    [ConfigurationSectionName("UnmonitoredButValidated")]
    public class UnmonitoredButValidatedOptions
    {
        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
    }
}