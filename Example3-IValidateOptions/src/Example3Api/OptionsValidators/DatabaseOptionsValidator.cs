using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Example3Api.Attributes;
using Example3Api.Constants;
using Example3Api.Options;
using Microsoft.Extensions.Options;

namespace Example3Api.OptionsValidators
{
    public class DatabaseOptionsValidator : IValidateOptions<DatabaseOptions>
    {
        public ValidateOptionsResult Validate(
            string name, 
            DatabaseOptions options
            )
        {
            var sectionName = typeof(DatabaseOptions).GetCustomAttribute<ConfigurationSectionNameAttribute>()?.SectionName
                ?? throw new ArgumentNullException(nameof(ConfigurationSectionNameAttribute));            
            
            if (options is null)
                return ValidateOptionsResult.Fail($"Configuration object is null for section '{sectionName}'.");
            
            var failureMessages = new List<string>();
            
            if (string.IsNullOrWhiteSpace(options.Comment1))
                failureMessages.Add($"{sectionName}.{nameof(options.Comment1)} is required.");
            
            if (string.IsNullOrWhiteSpace(options.DatabaseType))
                failureMessages.Add($"{sectionName}.{nameof(options.DatabaseType)} is required.");
            else
            {
                if (!DatabaseTypes.All.Value.Contains(options.DatabaseType, StringComparer.Ordinal))
                    failureMessages.Add($"{sectionName}.{nameof(options.DatabaseType)} is not one of the allowed values.");
            }

            if (string.IsNullOrWhiteSpace(options.Comment2))
                failureMessages.Add($"{sectionName}.{nameof(options.Comment2)} is required.");

            if (string.IsNullOrWhiteSpace(options.SchemaNames?.Schema1))
                failureMessages.Add($"{sectionName}.{nameof(options.SchemaNames)}.{nameof(options.SchemaNames.Schema1)} is required.");

            if (string.IsNullOrWhiteSpace(options.SchemaNames?.Schema2))
                failureMessages.Add($"{sectionName}.{nameof(options.SchemaNames)}.{nameof(options.SchemaNames.Schema2)} is required.");

            return failureMessages.Any()
                ? ValidateOptionsResult.Fail(failureMessages)
                : ValidateOptionsResult.Success;
        }
    }
}