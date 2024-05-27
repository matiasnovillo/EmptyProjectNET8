using System;
using System.ComponentModel.DataAnnotations;

namespace EmptyProject.Library.ModelAttributeValidator
{
    public class DecimalAttribute : ValidationAttribute
    {
        private string _NameToShow;
        private string _Name;
        private decimal _MinimumDecimalNumber;
        private decimal _MaximumDecimalNumber;
        private bool _Required;
        public DecimalAttribute(string NameToShow, string Name, bool Required, double DecimalMin, double DecimalMax)
        {
            try
            {
                _NameToShow = NameToShow;
                _Name = Name;
                _MinimumDecimalNumber = (decimal)DecimalMin;
                _MaximumDecimalNumber = (decimal)DecimalMax;

                _Required = Required;
            }
            catch (Exception) { throw; }
        }

        protected override ValidationResult IsValid(object objDecimal, ValidationContext validationContext)
        {
            try
            {
                if (_Required)
                {
                    if (objDecimal == null) 
                    {
                        return new ValidationResult($"[{_Name}] La variable {_NameToShow} es requerida");
                    }
                    else
                    {
                        if (objDecimal is not decimal)
                        {
                            return new ValidationResult($"[{_Name}] La variable {_NameToShow} no es un número decimal válido");
                        }
                        else
                        {
                            if ((decimal)objDecimal < decimal.MinValue ||
                                (decimal)objDecimal > decimal.MaxValue ||
                                (decimal)objDecimal < _MinimumDecimalNumber ||
                                (decimal)objDecimal > _MaximumDecimalNumber)
                            {
                                return new ValidationResult($"[{_Name}] La variable {_NameToShow} debe estar comprendida entre {_MinimumDecimalNumber} y {_MaximumDecimalNumber}");
                            }
                            else
                            {
                                return ValidationResult.Success;
                            }
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
