using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Example3Api.Attributes;
using Microsoft.Extensions.Options;

namespace Example3Api.Settings.Validators
{
    public class ConnectionStringsSettingsValidator : IValidateOptions<ConnectionStringsSettings>
    {
        public ValidateOptionsResult Validate(
            string name, 
            ConnectionStringsSettings settings
            )
        {
            var sectionName = typeof(ConnectionStringsSettings).GetCustomAttribute<ConfigurationSectionNameAttribute>()?.SectionName
                ?? throw new ArgumentNullException(nameof(ConfigurationSectionNameAttribute));  
            
            if (settings is null)
                return ValidateOptionsResult.Fail($"Configuration object is null for section '{sectionName}'.");

            var failureMessages = new List<string>();
            
            if (string.IsNullOrWhiteSpace(settings.Connection1))
                failureMessages.Add($"{sectionName}.{nameof(settings.Connection1)} is required.");
            
            if (string.IsNullOrWhiteSpace(settings.Connection2))
                failureMessages.Add($"{sectionName}.{nameof(settings.Connection2)} is required.");

            if (string.IsNullOrWhiteSpace(settings.Connection3))
                failureMessages.Add($"{sectionName}.{nameof(settings.Connection3)} is required.");

            return failureMessages.Any()
                ? ValidateOptionsResult.Fail(failureMessages)
                : ValidateOptionsResult.Success;
        }
    }
}