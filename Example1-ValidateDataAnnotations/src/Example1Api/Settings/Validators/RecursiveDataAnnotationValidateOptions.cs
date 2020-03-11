using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.Extensions.Options;
using RecursiveDataAnnotationsValidation;

namespace Example1Api.Settings.Validators
{
    public class RecursiveDataAnnotationValidateOptions<TOptions> 
        : IValidateOptions<TOptions>
        where TOptions : class
    {
        private readonly RecursiveDataAnnotationValidator _validator = new RecursiveDataAnnotationValidator();
        
        public RecursiveDataAnnotationValidateOptions(string optionsBuilderName)
        {
            Name = optionsBuilderName;
        }

        public ValidateOptionsResult Validate(string name, TOptions options)
        {
            if (Name != null && name != Name) return ValidateOptionsResult.Skip;
            
            var validationResults = new List<ValidationResult>();
            if (_validator.TryValidateObjectRecursive(
                options,
                new ValidationContext(options, serviceProvider: null, items: null),
                validationResults
                ))
            {
                return ValidateOptionsResult.Success;
            }

            var errors = validationResults
                .Select(r => 
                    $"Validation failed for members: '{string.Join(",", r.MemberNames)}' with the error: '{r.ErrorMessage}'."
                    )
                .ToList();
            return ValidateOptionsResult.Fail(errors);
        }

        public string Name { get; set; }
    }
}