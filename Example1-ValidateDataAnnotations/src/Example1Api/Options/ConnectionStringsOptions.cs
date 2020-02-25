using System.ComponentModel.DataAnnotations;
using Example1Api.Attributes;

namespace Example1Api.Options
{
    [ConfigurationSectionName("ConnectionStrings")]
    public class ConnectionStringsOptions
    {
        [Required]
        public string Connection1 { get; set; }
    }
}