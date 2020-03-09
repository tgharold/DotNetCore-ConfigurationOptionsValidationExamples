using System.Reflection;
using Example1Api.Attributes;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Example1Api.Extensions
{
    public static class IServiceCollectionExtensions
    {
        /// <summary>Bind a section of the appsettings.json to a POCO along with wiring up
        /// recursively-validated DataAnnotation validation.</summary>
        public static IServiceCollection AddValidatedSettings<T>(
            this IServiceCollection services,
            IConfiguration configuration
            ) where T : class, new()
        {
            var sectionName = GetSettingsSectionName<T>();
            
            var configurationSection = configuration.GetSection(sectionName);
            
            services.AddOptions<T>()
                .Bind(configurationSection)
                .RecursivelyValidateDataAnnotations();
            
            return services;
        }

        /// <summary>Bind a section of the appsettings.json to a POCO along with wiring up
        /// recursively-validated DataAnnotation validation. This variant also outputs the
        /// section as an IOptions<T> object.</summary>
        public static IServiceCollection AddValidatedSettings<T>(
            this IServiceCollection services,
            IConfiguration configuration,
            out IOptions<T> settings
            ) where T : class, new()
        {
            var sectionName = GetSettingsSectionName<T>();
            
            var configurationSection = configuration.GetSection(sectionName);
            
            services.AddOptions<T>()
                .Bind(configurationSection)
                .RecursivelyValidateDataAnnotations();

            // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/options?view=aspnetcore-3.1#use-di-services-to-configure-options
            // https://stackoverflow.com/questions/59097503/access-dependency-injection-objects-during-startup-configureservices-method
            // https://andrewlock.net/access-services-inside-options-and-startup-using-configureoptions/

            //TODO: Figure out how to eagerly validate and return options
            settings = null;
            
            return services;
        }

        /// <summary>Bind a section of the appsettings.json to a POCO without validation.</summary>
        public static IServiceCollection AddSettings<T>(
            this IServiceCollection services,
            IConfiguration configuration
            ) where T : class, new()
        {
            var sectionName = GetSettingsSectionName<T>();
            
            var configurationSection = configuration.GetSection(sectionName);
            
            services.AddOptions<T>()
                .Bind(configurationSection);
            
            return services;
        }

        /// <summary>Bind a section of the appsettings.json to a POCO without validation.
        /// This variant also returns the section as an IOptions<T> object.</summary>
        public static IServiceCollection AddSettings<T>(
            this IServiceCollection services,
            IConfiguration configuration,
            out IOptions<T> settings
            ) where T : class, new()
        {
            var sectionName = GetSettingsSectionName<T>();
            
            var configurationSection = configuration.GetSection(sectionName);
            
            services.AddOptions<T>()
                .Bind(configurationSection);
            
            settings = Options.Create(configurationSection.Get<T>());
            
            return services;
        }

        /// <summary>Get the section name within appsettings.json.  If the SettingsSectionNameAttribute
        /// is present, use that as the section name in the JSON.  Otherwise fall back to the type name.
        /// </summary>
        private static string GetSettingsSectionName<T>() where T : class
        {
            return typeof(T).GetCustomAttribute<SettingsSectionNameAttribute>()?.SectionName
                ?? typeof(T).Name;
        }
    }
}