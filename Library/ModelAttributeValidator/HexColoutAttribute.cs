using System;
using System.ComponentModel.DataAnnotations;

namespace EmptyProject.Library.ModelAttributeValidator
{
    public class HexColourAttribute : ValidationAttribute
    {
        private string _Name;
        private string _MinimumHexColourInStringFormat;
        private string _MaximumHexColourInStringFormat;
        private bool _Required;
        public HexColourAttribute(string Name, bool Required, string HexColourMinInStringFormat, string HexColourMaxInStringFormat)
        {
            _Name = Name;
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
                        return new ValidationResult($"La variable {_Name} es requerida");
                    }
                    else
                    {
                        if (Validator.IsHexColour(objHexColourInStringFormat.ToString().Replace("#","")) == false)
                        {
                            return new ValidationResult($"La variable {_Name} no es un color válido");
                        }
                        if (Validator.CompareStrings(objHexColourInStringFormat.ToString().Replace("#", ""), _MinimumHexColourInStringFormat) == 'B')
                        {
                            return new ValidationResult($"La variable {_Name} es menor que {_MinimumHexColourInStringFormat}");
                        }
                        if (Validator.CompareStrings(objHexColourInStringFormat.ToString().Replace("#", ""), _MaximumHexColourInStringFormat) == 'A')
                        {
                            return new ValidationResult($"La variable {_Name} es mayor que {_MaximumHexColourInStringFormat}");
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
