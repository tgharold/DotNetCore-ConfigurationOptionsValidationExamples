using System.Reflection;
using Example2Api.Attributes;
using Example2Api.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Example2Api.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddValidatedSettings<T>(
            this IServiceCollection services,
            IConfiguration configuration
            ) where T : class, ICanValidate
        {
            var sectionName = typeof(T).GetCustomAttribute<SettingsSectionNameAttribute>()?.SectionName
                ?? typeof(T).Name;
            
            var configurationSection = configuration.GetSection(sectionName);
            
            services.AddOptions<T>()
                .Bind(configurationSection)
                .Validate(x => x.IsValid(), "custom error");

            return services;
        }
    }
}