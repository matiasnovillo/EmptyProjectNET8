using System;
using System.ComponentModel.DataAnnotations;

namespace EmptyProject.Library.ModelAttributeValidator
{
    public class RequiredAttribute : ValidationAttribute
    {
        
        public RequiredAttribute()
        {
        }

        protected override ValidationResult IsValid(object obj, ValidationContext validationContext)
        {
            try
            {
                if (obj == null)
                {
                    return new ValidationResult($"La variable {validationContext.DisplayName} es requerida");
                }
                else
                {
                    return ValidationResult.Success;
                }
            }
            catch (Exception) { throw; }
        }
    }
}
