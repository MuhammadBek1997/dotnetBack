using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace FudballManagement.Application.Attributes;
public  class PhoneNumber : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        string PhoneNumber = (string)value;
        if (String.IsNullOrEmpty(PhoneNumber))
            return new ValidationResult("Please Enter the valid Phone Number");
        else
        {
            Regex regex = new Regex("^(?:\\+998([- ])?(90|91|93|94|95|98|99|33|50|97|71|20|88)([- ])?(\\d{3})([- ])?(\\d{2})([- ])?(\\d{2}))");

            return (regex.Match(PhoneNumber).Success) ? ValidationResult.Success
                : new ValidationResult("Please  enter the valid phone number");

        }
    }
}
