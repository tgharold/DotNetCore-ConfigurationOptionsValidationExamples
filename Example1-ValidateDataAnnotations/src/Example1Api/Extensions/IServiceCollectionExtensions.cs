using System;
using System.Reflection;
using Example1Api.Attributes;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Example1Api.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static T ConfigureAndValidateSection<T>(
            this IServiceCollection services,
            IConfiguration configuration
            ) where T : class
        {
            var sectionName = typeof(T).GetCustomAttribute<ConfigurationSectionNameAttribute>()?.SectionName
                ?? throw new ArgumentNullException(nameof(ConfigurationSectionNameAttribute));
            
            var configurationSection = configuration.GetSection(sectionName);
            
            //Notes:
            // - Validation code runs the first time an instance is requested from the container
            // - https://github.com/dotnet/extensions/issues/459 (eager validation, maybe in future .NET Core)
            // - https://stackoverflow.com/a/51693303 (discussion)

            services.AddOptions<T>()
                .Bind(configurationSection)
                .ValidateDataAnnotations()
                ;
            
            return configurationSection.Get<T>();
        }

    }
}