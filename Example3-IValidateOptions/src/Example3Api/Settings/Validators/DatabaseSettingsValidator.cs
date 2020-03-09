using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Example3Api.Attributes;
using Example3Api.Constants;
using Microsoft.Extensions.Options;

namespace Example3Api.Settings.Validators
{
    public class DatabaseSettingsValidator : IValidateOptions<DatabaseSettings>
    {
        public ValidateOptionsResult Validate(
            string name, 
            DatabaseSettings settings
            )
        {
            var sectionName = typeof(DatabaseSettings).GetCustomAttribute<SettingsSectionNameAttribute>()?.SectionName
                ?? throw new ArgumentNullException(nameof(SettingsSectionNameAttribute));            
            
            if (settings is null)
                return ValidateOptionsResult.Fail($"Configuration object is null for section '{sectionName}'.");
            
            var failureMessages = new List<string>();
            
            if (string.IsNullOrWhiteSpace(settings.Comment1))
                failureMessages.Add($"{sectionName}.{nameof(settings.Comment1)} is required.");
            
            if (string.IsNullOrWhiteSpace(settings.DatabaseType))
                failureMessages.Add($"{sectionName}.{nameof(settings.DatabaseType)} is required.");
            else
            {
                if (!DatabaseTypes.All.Value.Contains(settings.DatabaseType, StringComparer.Ordinal))
                    failureMessages.Add($"{sectionName}.{nameof(settings.DatabaseType)} is not one of the allowed values.");
            }

            if (string.IsNullOrWhiteSpace(settings.Comment2))
                failureMessages.Add($"{sectionName}.{nameof(settings.Comment2)} is required.");

            if (string.IsNullOrWhiteSpace(settings.SchemaNames?.Schema1))
                failureMessages.Add($"{sectionName}.{nameof(settings.SchemaNames)}.{nameof(settings.SchemaNames.Schema1)} is required.");

            if (string.IsNullOrWhiteSpace(settings.SchemaNames?.Schema2))
                failureMessages.Add($"{sectionName}.{nameof(settings.SchemaNames)}.{nameof(settings.SchemaNames.Schema2)} is required.");

            return failureMessages.Any()
                ? ValidateOptionsResult.Fail(failureMessages)
                : ValidateOptionsResult.Success;
        }
    }
}