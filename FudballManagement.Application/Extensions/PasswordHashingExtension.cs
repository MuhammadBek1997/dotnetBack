using System.Security.Cryptography;
using System.Text;

namespace FudballManagement.Application.Extensions;
public static class PasswordHashingExtension
{
    public static string Encrypt(this string password)
    {
        using(SHA256 sha256 = SHA256.Create())
        {
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            var hashedPassowrd = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            return hashedPassowrd;
        }
    }
}
