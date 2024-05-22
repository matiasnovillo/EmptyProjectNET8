using System;
using System.ComponentModel.DataAnnotations;

namespace EmptyProject.Library.ModelAttributeValidator
{
    public class StringAttribute : ValidationAttribute
    {
        
        private int _MinimumLength;
        private int _MaximumLength;
        private string _RegexString;
        private bool _Required;
        public StringAttribute(bool Required, int MinimumLength, int MaximumLength, string RegexString)
        {
            try
            {
                _MinimumLength = MinimumLength;
                _MaximumLength = MaximumLength;
                _RegexString = RegexString;

                _Required = Required;
            }
            catch (Exception) { throw; }
        }

        protected override ValidationResult IsValid(object objString, ValidationContext validationContext)
        {
            try
            {
                if (_Required)
                {
                    if (objString == null) 
                    {
                        return new ValidationResult($"La variable {validationContext.DisplayName} es requerida");
                    }
                    else
                    {
                        return ValidationResult.Success;
                    }
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
