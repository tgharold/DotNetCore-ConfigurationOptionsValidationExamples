using Example1Api.OptionsValidators;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Example1Api.Extensions
{
    public static class OptionsBuilderDataAnnotationsExtensions
    {
        public static OptionsBuilder<TOptions> RecursivelyValidateDataAnnotations<TOptions>(
            this OptionsBuilder<TOptions> optionsBuilder
            ) where TOptions : class
        {
            optionsBuilder.Services.AddSingleton<IValidateOptions<TOptions>>(
                new RecursiveDataAnnotationValidateOptions<TOptions>(
                    optionsBuilder.Name
                ));
            return optionsBuilder;
        }
    }
}