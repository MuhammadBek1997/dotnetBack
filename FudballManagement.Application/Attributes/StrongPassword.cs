using FudballManagement.Application.Validators;
using System.ComponentModel.DataAnnotations;

namespace FudballManagement.Application.Attributes;
public class StrongPassword : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null) return new ValidationResult("you must enter Password");
        else
        {
            string password = value.ToString();
            if (password.Length < 8)
                return new ValidationResult("Password must include at least 8 character");
            else if (password.Length > 16)
                return new ValidationResult("Password must be under the 16 character ");
            var result = PasswordValidator.IsStrong(password);

            if (result.IsValid is false) return new ValidationResult(result.Message);
            else return ValidationResult.Success;
        }
    }
}
