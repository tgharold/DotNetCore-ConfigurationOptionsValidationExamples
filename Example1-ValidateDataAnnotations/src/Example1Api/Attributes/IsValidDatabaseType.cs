using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Example1Api.Constants;

namespace Example1Api.Attributes
{
    public class IsValidDatabaseType : ValidationAttribute
    {
        //TODO: Figure out why the error message displays without the member (property) name.
        //   Example: DataAnnotation validation failed for members: '' with the error: 'Value is required.'.
        
        public IsValidDatabaseType(
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