using System.Reflection;
using Example3Api.Attributes;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Example3Api.Extensions
{
    public static class IServiceCollectionExtensions
    {
        /// <summary>Bind a section of the appsettings.json to a POCO along with wiring up
        /// IValidateOptions<T> validation.</summary>
        public static IServiceCollection AddValidatedSettings<T, TValidator>(
            this IServiceCollection services,
            IConfiguration configuration
            )
            where T : class
            where TValidator : class, IValidateOptions<T>
        {
            var sectionName = GetSectionName<T>();

            var configurationSection = configuration.GetSection(sectionName);

            services.AddOptions<T>()
                .Bind(configurationSection);

            services.AddSingleton<IValidateOptions<T>, TValidator>();

            return services;
        }

        /// <summary>Bind a section of the appsettings.json to a POCO without validation.</summary>
        public static IServiceCollection AddSettings<T>(
            this IServiceCollection services,
            IConfiguration configuration
            )
            where T : class
        {
            var sectionName = GetSectionName<T>();

            var configurationSection = configuration.GetSection(sectionName);

            services.AddOptions<T>()
                .Bind(configurationSection);

            return services;
        }

        private static string GetSectionName<T>()
            where T : class
        {
            // Assume that if there is no custom attribute that the section is named after the class
            return typeof(T).GetCustomAttribute<ConfigurationSectionNameAttribute>()?.SectionName
                   ?? typeof(T).Name;
        }
    }
}