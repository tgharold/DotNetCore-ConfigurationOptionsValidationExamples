using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Example3Api.Attributes;
using Example3Api.Options;
using Microsoft.Extensions.Options;

namespace Example3Api.OptionsValidators
{
    public class MonitoredSettingsOptionsValidator : IValidateOptions<MonitoredSettingsOptions>
    {
        public ValidateOptionsResult Validate(
            string name, 
            MonitoredSettingsOptions options
            )
        {
            var sectionName = typeof(MonitoredSettingsOptions).GetCustomAttribute<ConfigurationSectionNameAttribute>()?.SectionName
                ?? throw new ArgumentNullException(nameof(ConfigurationSectionNameAttribute));  
            
            if (options is null)
                return ValidateOptionsResult.Fail($"Configuration object is null for section '{sectionName}'.");

            var failureMessages = new List<string>();
            
            if (string.IsNullOrWhiteSpace(options.MonitorA))
                failureMessages.Add($"{sectionName}.{nameof(options.MonitorA)} is required.");
            
            if (string.IsNullOrWhiteSpace(options.MonitorB))
                failureMessages.Add($"{sectionName}.{nameof(options.MonitorB)} is required.");

            return failureMessages.Any()
                ? ValidateOptionsResult.Fail(failureMessages)
                : ValidateOptionsResult.Success;
        }
    }
}