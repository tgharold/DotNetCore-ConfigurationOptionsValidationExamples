using System;

namespace Example2Api.Attributes
{
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