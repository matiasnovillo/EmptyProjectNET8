using System;
using System.ComponentModel.DataAnnotations;

namespace EmptyProject.Library.ModelAttributeValidator
{
    public class DateTimeAttribute : ValidationAttribute
    {
        private string _NameToShow;
        private string _Name;
        private DateTime _MinimumDateTime;
        private DateTime _MaximumDateTime;
        private bool _Required;
        public DateTimeAttribute(string NameToShow, string Name, bool Required, string DateTimeMinValue, string DateTimeMaxValue)
        {
            try
            {
                _NameToShow = NameToShow;
                _Name = Name;
                _MinimumDateTime = System.Convert.ToDateTime(DateTimeMinValue);
                _MaximumDateTime = System.Convert.ToDateTime(DateTimeMaxValue);

                _Required = Required;
            }
            catch (Exception) { throw; }
        }

        protected override ValidationResult IsValid(object objDateTime, ValidationContext validationContext)
        {
            try
            {
                if (_Required)
                {
                    if (objDateTime == null)
                    {
                        return new ValidationResult($"[{_Name}] La variable {_NameToShow} es requerida");
                    }
                    else
                    {
                        if ((DateTime)objDateTime < _MinimumDateTime || (DateTime)objDateTime > _MaximumDateTime)
                        {
                            return new ValidationResult($@"[{_Name}] La variable {_NameToShow} 
debe estar entre {_MinimumDateTime.ToString("dd/MM/yyyy HH:mm:ss")} y 
{_MaximumDateTime.ToString("dd/MM/yyyy HH:mm:ss")}.");
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
