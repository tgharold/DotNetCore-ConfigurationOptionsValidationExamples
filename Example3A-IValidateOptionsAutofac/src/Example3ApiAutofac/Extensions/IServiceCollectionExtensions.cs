using System;
using System.Reflection;
using Example3ApiAutofac.Attributes;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Example3ApiAutofac.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static T ConfigureAndValidateSection<T, TValidator>(
            this IServiceCollection services,
            IConfiguration configuration
            ) 
            where T : class
            where TValidator : class, IValidateOptions<T>
        {
            var sectionName = typeof(T).GetCustomAttribute<ConfigurationSectionNameAttribute>()?.SectionName
                ?? throw new ArgumentNullException(nameof(ConfigurationSectionNameAttribute));
            
            var configurationSection = configuration.GetSection(sectionName);

            services.AddOptions<T>()
                .Bind(configurationSection);
            
            services.AddSingleton<IValidateOptions<T>, TValidator>();
        
            return configurationSection.Get<T>();
        }

        public static T ConfigureSection<T>(
            this IServiceCollection services,
            IConfiguration configuration
            ) 
            where T : class
        {
            var sectionName = typeof(T).GetCustomAttribute<ConfigurationSectionNameAttribute>()?.SectionName
                ?? throw new ArgumentNullException(nameof(ConfigurationSectionNameAttribute));
            
            var configurationSection = configuration.GetSection(sectionName);

            services.AddOptions<T>()
                .Bind(configurationSection);
            
            return configurationSection.Get<T>();
        }

    }
}