using System;
using System.ComponentModel.DataAnnotations;

namespace EmptyProject.Library.ModelAttributeValidator
{
    public class TimeSpanAttribute : ValidationAttribute
    {
        private string _PropertyName;
        private TimeSpan _MinimumTimeSpan;
        private TimeSpan _MaximumTimeSpan;
        private bool _Required;
        public TimeSpanAttribute(string PropertyName, bool Required, string TimeSpanMin, string TimeSpanMax)
        {
            try
            {
                _PropertyName = PropertyName;

                _MinimumTimeSpan = TimeSpan.Parse(TimeSpanMin);
                _MaximumTimeSpan = TimeSpan.Parse(TimeSpanMax);

                _Required = Required;
            }
            catch (Exception) { throw; }
        }

        public override bool IsValid(object? objTimeSpan)
        {
            try
            {
                if (_Required)
                {
                    if (objTimeSpan == null) { throw new Exception($"{_PropertyName} is empty"); }
                }

                if ((TimeSpan)objTimeSpan < _MinimumTimeSpan || (TimeSpan)objTimeSpan > _MaximumTimeSpan)
                {
                    throw new Exception($"{_PropertyName} must be inside {_MinimumTimeSpan} and {_MaximumTimeSpan}");
                }
                return true;
            }
            catch (Exception) { throw; }
        }
    }
}
