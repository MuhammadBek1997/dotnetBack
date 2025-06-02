namespace FudballManagement.Application.Validators;

public  static class PasswordValidator
{
    public static (bool IsValid, string Message) IsStrong(string password)
    {
        bool ContainDigit =  password.Any(char.IsDigit);
        bool ContainLetter = password.Any(char.IsLetter);
        bool IsUpper = password.Any(char.IsUpper);

        if (!ContainDigit || !ContainLetter || !IsUpper || password.Length < 8)
            return (IsValid: false,Message :"Password is not strong enough");
        return (IsValid: true, Message: "Strong Password");
    }
}
