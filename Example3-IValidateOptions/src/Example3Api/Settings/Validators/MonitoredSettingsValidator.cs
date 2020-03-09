using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Example3Api.Attributes;
using Microsoft.Extensions.Options;

namespace Example3Api.Settings.Validators
{
    public class MonitoredSettingsValidator : IValidateOptions<MonitoredSettings>
    {
        public ValidateOptionsResult Validate(
            string name, 
            MonitoredSettings settings
            )
        {
            var sectionName = typeof(MonitoredSettings).GetCustomAttribute<ConfigurationSectionNameAttribute>()?.SectionName
                ?? throw new ArgumentNullException(nameof(ConfigurationSectionNameAttribute));  
            
            if (settings is null)
                return ValidateOptionsResult.Fail($"Configuration object is null for section '{sectionName}'.");

            var failureMessages = new List<string>();
            
            if (string.IsNullOrWhiteSpace(settings.MonitorA))
                failureMessages.Add($"{sectionName}.{nameof(settings.MonitorA)} is required.");
            
            if (string.IsNullOrWhiteSpace(settings.MonitorB))
                failureMessages.Add($"{sectionName}.{nameof(settings.MonitorB)} is required.");

            return failureMessages.Any()
                ? ValidateOptionsResult.Fail(failureMessages)
                : ValidateOptionsResult.Success;
        }
    }
}