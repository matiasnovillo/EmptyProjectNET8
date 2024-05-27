using System;
using System.ComponentModel.DataAnnotations;

namespace EmptyProject.Library.ModelAttributeValidator
{
    public class KeyAttribute : ValidationAttribute
    {
        private string _NameToShow;
        private string _Name;
        public KeyAttribute(string NameToShow, string Name)
        {
            try
            {
                _NameToShow = NameToShow;
                _Name = Name;
            }
            catch (Exception) { throw; }
        }

        protected override ValidationResult IsValid(object objPrimaryKey, ValidationContext validationContext)
        {
            try
            {
                if (objPrimaryKey == null) 
                {
                    return new ValidationResult($"[{_Name}] La variable {_NameToShow} es requerida");
                }
                else
                {
                    if (Convert.ToInt32(objPrimaryKey) <= 0)
                    {
                        return new ValidationResult($"[{_Name}] La variable {_NameToShow} debe ser mayor a 0");
                    }
                    else
                    {
                        return ValidationResult.Success;
                    }
                }
            }
            catch (Exception) { throw; }
        }
    }
}
