using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Example3Api.Attributes;
using Example3Api.Options;
using Microsoft.Extensions.Options;

namespace Example3Api.OptionsValidators
{
    public class ConnectionStringsOptionsValidator : IValidateOptions<ConnectionStringsOptions>
    {
        public ValidateOptionsResult Validate(
            string name, 
            ConnectionStringsOptions options
            )
        {
            var sectionName = typeof(ConnectionStringsOptions).GetCustomAttribute<ConfigurationSectionNameAttribute>()?.SectionName
                ?? throw new ArgumentNullException(nameof(ConfigurationSectionNameAttribute));  
            
            if (options is null)
                return ValidateOptionsResult.Fail($"Configuration object is null for section '{sectionName}'.");

            var failureMessages = new List<string>();
            
            if (string.IsNullOrWhiteSpace(options.Connection1))
                failureMessages.Add($"{sectionName}.{nameof(options.Connection1)} is required.");
            
            if (string.IsNullOrWhiteSpace(options.Connection2))
                failureMessages.Add($"{sectionName}.{nameof(options.Connection2)} is required.");

            if (string.IsNullOrWhiteSpace(options.Connection3))
                failureMessages.Add($"{sectionName}.{nameof(options.Connection3)} is required.");

            return failureMessages.Any()
                ? ValidateOptionsResult.Fail(failureMessages)
                : ValidateOptionsResult.Success;
        }
    }
}