using System;
using System.ComponentModel.DataAnnotations;

namespace EmptyProject.Library.ModelAttributeValidator
{
    public class RequiredAttribute : ValidationAttribute
    {
        private string _Name;
        
        public RequiredAttribute(string Name)
        {
            _Name = Name;
        }

        protected override ValidationResult IsValid(object obj, ValidationContext validationContext)
        {
            try
            {
                if (obj == null)
                {
                    return new ValidationResult($"La variable {_Name} es requerida");
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
