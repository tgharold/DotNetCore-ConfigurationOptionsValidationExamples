using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Example3Api.Attributes;
using Example3Api.Options;
using Microsoft.Extensions.Options;

namespace Example3Api.OptionsValidators
{
    public class UnmonitoredButValidatedOptionsValidator : IValidateOptions<UnmonitoredButValidatedOptions>
    {
        public ValidateOptionsResult Validate(
            string name, 
            UnmonitoredButValidatedOptions options
            )
        {
            var sectionName = typeof(UnmonitoredButValidatedOptions).GetCustomAttribute<ConfigurationSectionNameAttribute>()?.SectionName
                ?? throw new ArgumentNullException(nameof(ConfigurationSectionNameAttribute));  
            
            if (options is null)
                return ValidateOptionsResult.Fail($"Configuration object is null for section '{sectionName}'.");

            var failureMessages = new List<string>();
            
            if (string.IsNullOrWhiteSpace(options.OptionA))
                failureMessages.Add($"{sectionName}.{nameof(options.OptionA)} is required.");
            
            if (string.IsNullOrWhiteSpace(options.OptionB))
                failureMessages.Add($"{sectionName}.{nameof(options.OptionB)} is required.");

            if (string.IsNullOrWhiteSpace(options.OptionC))
                failureMessages.Add($"{sectionName}.{nameof(options.OptionC)} is required.");

            return failureMessages.Any()
                ? ValidateOptionsResult.Fail(failureMessages)
                : ValidateOptionsResult.Success;
        }
    }
}