using System;
using System.ComponentModel.DataAnnotations;

namespace EmptyProject.Library.ModelAttributeValidator
{
    public class DecimalAttribute : ValidationAttribute
    {
        private string _PropertyName;
        private decimal _MinimumDecimalNumber;
        private decimal _MaximumDecimalNumber;
        private bool _Required;
        public DecimalAttribute(string PropertyName, bool Required, double DecimalMin, double DecimalMax)
        {
            try
            {
                _PropertyName = PropertyName;

                if (DecimalMin < (double)decimal.MinValue || DecimalMin > (double)decimal.MaxValue)
                {
                    throw new Exception("The validator MinimumDecimalNumber must be inside decimalDBMin and decimalDBMax");
                }
                if (DecimalMax < (double)decimal.MinValue || DecimalMax > (double)decimal.MaxValue)
                {
                    throw new Exception("The validator MaximumDecimalNumber must be inside decimalDBMin and decimalDBMax");
                }
                _MinimumDecimalNumber = (decimal)DecimalMin;
                _MaximumDecimalNumber = (decimal)DecimalMax;

                _Required = Required;
            }
            catch (Exception) { throw; }
        }

        public override bool IsValid(object? objDecimal)
        {
            try
            {
                if (_Required)
                {
                    if (objDecimal == null) { throw new Exception($"{_PropertyName} is empty"); }
                }
                if (objDecimal.GetType() != typeof(decimal)) { throw new Exception($"{_PropertyName} is not a valid decimal number"); }

                if ((decimal)objDecimal < decimal.MinValue || (decimal)objDecimal > decimal.MaxValue)
                {
                    throw new Exception($"{_PropertyName} must be inside decimalDBMin and decimalDBMax");
                }
                if ((decimal)objDecimal < _MinimumDecimalNumber || (decimal)objDecimal > _MaximumDecimalNumber)
                {
                    throw new Exception($"{_PropertyName} must be inside {_MinimumDecimalNumber} and {_MaximumDecimalNumber}");
                }
                return true;
            }
            catch (Exception) { throw; }
        }
    }
}
