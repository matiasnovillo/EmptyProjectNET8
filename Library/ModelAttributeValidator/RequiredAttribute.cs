using System;
using System.ComponentModel.DataAnnotations;

namespace EmptyProject.Library.ModelAttributeValidator
{
    public class RequiredAttribute : ValidationAttribute
    {
        private string _PropertyName;
        public RequiredAttribute(string PropertyName)
        {
            _PropertyName = PropertyName;
        }

        public override bool IsValid(object? obj)
        {
            try
            {
                if (obj == null)
                { throw new Exception($"{_PropertyName} is empty"); }

                return true;
            }
            catch (Exception) { throw; }
        }
    }
}
