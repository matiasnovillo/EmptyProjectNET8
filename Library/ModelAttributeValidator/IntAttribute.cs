using System;
using System.ComponentModel.DataAnnotations;

namespace EmptyProject.Library.ModelAttributeValidator
{
    public class IntAttribute : ValidationAttribute
    {
        private string _Name;
        private int _MinimumInt;
        private int _MaximumInt;
        private bool _Required;

        public IntAttribute(string Name, bool Required, int IntMin, int IntMax)
        {
            _Name = Name;
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
                        return new ValidationResult($"La variable {_Name} es requerida");
                    }
                    else
                    {
                        if (Convert.ToInt32(objInt) <= _MinimumInt || Convert.ToInt32(objInt) >= _MaximumInt)
                        {
                            return new ValidationResult($"La variable {_Name} debe ser mayor que {_MinimumInt} y menor que {_MaximumInt}");
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
