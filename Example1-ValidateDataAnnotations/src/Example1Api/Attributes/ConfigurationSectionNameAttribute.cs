using System;

namespace Example1Api.Attributes
{
    /// <summary>Use this when the section name in the appsettings.json file does not match
    /// the name of the options class name.</summary>
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