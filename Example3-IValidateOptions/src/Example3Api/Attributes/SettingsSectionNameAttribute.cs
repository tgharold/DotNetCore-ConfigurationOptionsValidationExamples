using System;

namespace Example3Api.Attributes
{
    /// <summary>Used when the section name in the configuration file does not match the
    /// class name of the options/configuration object.</summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class SettingsSectionNameAttribute : Attribute
    {
        public SettingsSectionNameAttribute(string sectionName)
        {
            SectionName = sectionName;
        }
        
        public string SectionName { get; }
    }
}