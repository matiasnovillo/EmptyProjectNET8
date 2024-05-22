using System;
using System.ComponentModel.DataAnnotations;

namespace EmptyProject.Library.ModelAttributeValidator
{
    public class TimeSpanAttribute : ValidationAttribute
    {
        private TimeSpan _MinimumTimeSpan;
        private TimeSpan _MaximumTimeSpan;
        private bool _Required;

        public TimeSpanAttribute(bool Required, string TimeSpanMin, string TimeSpanMax)
        {
            try
            {

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
                        //throw new Exception($"{_PropertyName} is empty"); 
                    }
                }

                if ((TimeSpan)objTimeSpan < _MinimumTimeSpan || (TimeSpan)objTimeSpan > _MaximumTimeSpan)
                {
                    //throw new Exception($"{_PropertyName} must be inside {_MinimumTimeSpan} and {_MaximumTimeSpan}");
                }
                return true;
            }
            catch (Exception) { throw; }
        }
    }
}
