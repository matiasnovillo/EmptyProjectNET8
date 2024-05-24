using System;
using System.ComponentModel.DataAnnotations;

namespace EmptyProject.Library.ModelAttributeValidator
{
    public class StringAttribute : ValidationAttribute
    {
        private string _Name;
        private int _MinimumLength;
        private int _MaximumLength;
        private string _RegexString;
        private bool _Required;
        public StringAttribute(string Name, bool Required, int MinimumLength, int MaximumLength, string RegexString)
        {
            try
            {
                _Name = Name;
                _MinimumLength = MinimumLength;
                _MaximumLength = MaximumLength;
                _RegexString = RegexString;

                _Required = Required;
            }
            catch (Exception) { throw; }
        }

        protected override ValidationResult IsValid(object objString, ValidationContext validationContext)
        {
            try
            {
                if (_Required)
                {
                    if (objString == null) 
                    {
                        return new ValidationResult($"La variable {_Name} es requerida");
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(objString.ToString()))
                        {
                            return new ValidationResult($"La variable {_Name} es requerida");
                        }
                        else
                        {
                            if (validationContext.DisplayName.EndsWith("URL") && !objString.ToString().StartsWith("http"))
                            {
                                return new ValidationResult($"La variable {_Name} no es una URL válida, debe empezar con http");
                            }
                            else if (validationContext.DisplayName.EndsWith("File") && objString.ToString() == "")
                            {
                                return new ValidationResult($"La variable {_Name} no es válida, suba un archivo");
                            }
                            else if (validationContext.DisplayName.EndsWith("Email") && !objString.ToString().Contains("@"))
                            {
                                return new ValidationResult($"La variable {_Name} no es válida como correo");
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
