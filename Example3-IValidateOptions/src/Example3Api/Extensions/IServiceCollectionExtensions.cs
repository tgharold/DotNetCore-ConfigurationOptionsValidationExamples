using System;
using System.Reflection;
using Example3Api.Attributes;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Example3Api.Extensions
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
                .Bind(configurationSection);
        
                /* For the IValidateOptions approach, we don't wire up validation here.  Instead we just inject those
                 * validators into the DI system in Startup.ConfigureServices():
                 *
                 *   services.AddSingleton<IValidateOptions<T>, TValidator>();
                 *
                 * It reduces the need for a marker/trait interface like was used in Example 2.
                 */

            return configurationSection.Get<T>();
        }

    }
}