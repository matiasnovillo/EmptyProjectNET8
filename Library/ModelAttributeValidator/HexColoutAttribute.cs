using System;
using System.ComponentModel.DataAnnotations;

namespace EmptyProject.Library.ModelAttributeValidator
{
    public class HexColourAttribute : ValidationAttribute
    {
        
        private string _MinimumHexColourInStringFormat;
        private string _MaximumHexColourInStringFormat;
        private bool _Required;
        public HexColourAttribute(bool Required, string HexColourMinInStringFormat, string HexColourMaxInStringFormat)
        {
            _MinimumHexColourInStringFormat = HexColourMinInStringFormat;
            _MaximumHexColourInStringFormat = HexColourMaxInStringFormat;

            _Required = Required;
        }
        protected override ValidationResult IsValid(object? objHexColourInStringFormat, ValidationContext validationContext)
        {
            try
            {
                if (_Required)
                {
                    if (objHexColourInStringFormat == null)
                    {
                        return new ValidationResult($"La variable {validationContext.DisplayName} es requerida");
                    }
                    else
                    {
                        if (Validator.IsHexColour(objHexColourInStringFormat.ToString()) == false)
                        {
                            return new ValidationResult($"La variable {validationContext.DisplayName} no es un color válido");
                        }
                        if (Validator.CompareStrings(objHexColourInStringFormat.ToString(), _MinimumHexColourInStringFormat) == 'B')
                        {
                            return new ValidationResult($"La variable {validationContext.DisplayName} es menor que {_MinimumHexColourInStringFormat}");
                        }
                        if (Validator.CompareStrings(objHexColourInStringFormat.ToString(), _MaximumHexColourInStringFormat) == 'A')
                        {
                            return new ValidationResult($"La variable {validationContext.DisplayName} es mayor que {_MaximumHexColourInStringFormat}");
                        }

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
