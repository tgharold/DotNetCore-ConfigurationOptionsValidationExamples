using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Example3Api.Attributes;
using Microsoft.Extensions.Options;

namespace Example3Api.Settings.Validators
{
    public class UnmonitoredButValidatedSettingsValidator : IValidateOptions<UnmonitoredButValidatedSettings>
    {
        public ValidateOptionsResult Validate(
            string name, 
            UnmonitoredButValidatedSettings settings
            )
        {
            var sectionName = typeof(UnmonitoredButValidatedSettings).GetCustomAttribute<ConfigurationSectionNameAttribute>()?.SectionName
                ?? throw new ArgumentNullException(nameof(ConfigurationSectionNameAttribute));  
            
            if (settings is null)
                return ValidateOptionsResult.Fail($"Configuration object is null for section '{sectionName}'.");

            var failureMessages = new List<string>();
            
            if (string.IsNullOrWhiteSpace(settings.OptionA))
                failureMessages.Add($"{sectionName}.{nameof(settings.OptionA)} is required.");
            
            if (string.IsNullOrWhiteSpace(settings.OptionB))
                failureMessages.Add($"{sectionName}.{nameof(settings.OptionB)} is required.");

            if (string.IsNullOrWhiteSpace(settings.OptionC))
                failureMessages.Add($"{sectionName}.{nameof(settings.OptionC)} is required.");

            return failureMessages.Any()
                ? ValidateOptionsResult.Fail(failureMessages)
                : ValidateOptionsResult.Success;
        }
    }
}