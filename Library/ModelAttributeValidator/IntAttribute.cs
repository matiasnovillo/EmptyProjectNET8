using System;
using System.ComponentModel.DataAnnotations;

namespace EmptyProject.Library.ModelAttributeValidator
{
    public class IntAttribute : ValidationAttribute
    {
        private int _MinimumInt;
        private int _MaximumInt;
        private bool _Required;

        public IntAttribute(bool Required, int IntMin, int IntMax)
        {
            _MinimumInt = IntMin;
            _MaximumInt = IntMax;
            _Required = Required;
        }

        protected override ValidationResult IsValid(object objInt, ValidationContext validationContext)
        {
            try
            {
                if (_Required)
                {
                    if (objInt == null)
                    {
                        return new ValidationResult($"La variable {validationContext.DisplayName} es requerida");
                    }
                    else
                    {
                        if (Convert.ToInt32(objInt) < int.MinValue || Convert.ToInt32(objInt) > int.MaxValue)
                        {
                            return new ValidationResult($"La variable {validationContext.DisplayName} debe ser menor que {_MaximumInt} y mayor que {_MinimumInt}");
                        }
                        else
                        {
                            return ValidationResult.Success;
                        }
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
