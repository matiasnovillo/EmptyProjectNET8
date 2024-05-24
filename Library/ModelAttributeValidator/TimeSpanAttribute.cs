using System;
using System.ComponentModel.DataAnnotations;

namespace EmptyProject.Library.ModelAttributeValidator
{
    public class TimeSpanAttribute : ValidationAttribute
    {
        private string _Name;
        private TimeSpan _MinimumTimeSpan;
        private TimeSpan _MaximumTimeSpan;
        private bool _Required;

        public TimeSpanAttribute(string Name, bool Required, string TimeSpanMin, string TimeSpanMax)
        {
            try
            {
                _Name = Name;
                _MinimumTimeSpan = TimeSpan.Parse(TimeSpanMin);
                _MaximumTimeSpan = TimeSpan.Parse(TimeSpanMax);

                _Required = Required;
            }
            catch (Exception) { throw; }
        }

        protected override ValidationResult IsValid(object objTimeSpan, ValidationContext validationContext)
        {
            try
            {
                if (_Required)
                {
                    if (objTimeSpan == null) 
                    {
                        return new ValidationResult($"La variable {_Name} es requerida");
                    }
                    else
                    {
                        if ((TimeSpan)objTimeSpan < _MinimumTimeSpan || (TimeSpan)objTimeSpan > _MaximumTimeSpan)
                        {
                            return new ValidationResult($"La variable {_Name} debe estar entre {_MinimumTimeSpan} y {_MaximumTimeSpan}");
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
