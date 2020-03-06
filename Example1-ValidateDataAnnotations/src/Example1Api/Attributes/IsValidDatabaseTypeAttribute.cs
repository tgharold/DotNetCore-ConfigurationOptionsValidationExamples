using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Example1Api.Constants;

namespace Example1Api.Attributes
{
    public class IsValidDatabaseTypeAttribute : ValidationAttribute
    {
        //TODO: Figure out why the error message displays without the member (property) name.
        /* This may be tricky as annotations can't get the field info for what they are attached to
         * Example: DataAnnotation validation failed for members: '' with the error: 'Value is required.'.
         * It may be the responsibility of the RecursiveDataAnnotationValidator class to get a good name.
         */
        
        public IsValidDatabaseTypeAttribute(
            bool allowNull = false
            )
        {
            AllowNull = allowNull;
        }

        private bool AllowNull { get; }

        protected override ValidationResult IsValid(
            object value, 
            ValidationContext validationContext
            )
        {
            if (value is null)
            {
                if (AllowNull) return ValidationResult.Success;
                return new ValidationResult("Value is required.");
            }

            var valueString = value.ToString();
            
            return DatabaseTypes.All.Value.Contains(valueString, StringComparer.Ordinal) 
                ? ValidationResult.Success 
                : new ValidationResult("Value is not one of the allowed values.");
        }
    }
}