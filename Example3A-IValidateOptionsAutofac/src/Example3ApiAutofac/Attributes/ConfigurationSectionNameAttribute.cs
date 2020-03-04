using System;

namespace Example3ApiAutofac.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ConfigurationSectionNameAttribute : Attribute
    {
        public ConfigurationSectionNameAttribute(string sectionName)
        {
            SectionName = sectionName;
        }
        
        public string SectionName { get; }
    }
}