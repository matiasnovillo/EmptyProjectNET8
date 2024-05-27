using System;
using System.ComponentModel.DataAnnotations;

namespace EmptyProject.Library.ModelAttributeValidator
{
    public class RequiredAttribute : ValidationAttribute
    {
        private string _NameToShow;
        private string _Name;
        
        public RequiredAttribute(string NameToShow, string Name)
        {
            _NameToShow = NameToShow;
            _Name = Name;
        }

        protected override ValidationResult IsValid(object obj, ValidationContext validationContext)
        {
            try
            {
                if (obj == null)
                {
                    return new ValidationResult($"[{_Name}] La variable {_NameToShow} es requerida");
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
