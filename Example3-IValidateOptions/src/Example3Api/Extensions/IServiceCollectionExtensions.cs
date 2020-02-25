using System;
using System.Reflection;
using Example3Api.Attributes;
using Example3Api.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Example3Api.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static T ConfigureAndValidateSection<T>(
            this IServiceCollection services,
            IConfiguration configuration
            ) where T : class, ICanValidate
        {
            var sectionName = typeof(T).GetCustomAttribute<ConfigurationSectionNameAttribute>()?.SectionName
                ?? throw new ArgumentNullException(nameof(ConfigurationSectionNameAttribute));
            
            var configurationSection = configuration.GetSection(sectionName);
            
            services.AddOptions<T>()
                .Bind(configurationSection)
                .Validate(x => x.IsValid(), "custom error");

            return configurationSection.Get<T>();
        }

    }
}