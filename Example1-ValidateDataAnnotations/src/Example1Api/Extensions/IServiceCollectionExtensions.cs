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
            
            services.AddOptions<T>()
                .Bind(configurationSection)
                .RecursivelyValidateDataAnnotations()
                ;
            
            return configurationSection.Get<T>();
        }

    }
}