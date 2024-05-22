using System;
using System.ComponentModel.DataAnnotations;

namespace EmptyProject.Library.ModelAttributeValidator
{
    public class KeyAttribute : ValidationAttribute
    {
        
        public KeyAttribute()
        {
            try
            {
            }
            catch (Exception) { throw; }
        }

        protected override ValidationResult IsValid(object objPrimaryKey, ValidationContext validationContext)
        {
            try
            {
                if (objPrimaryKey == null) 
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
