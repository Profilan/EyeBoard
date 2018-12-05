
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace EyeBoard.Models.Identity
{
    public class CustomPasswordHasher : IPasswordHasher
    {
        const string SALT = "%$#HD&^5637*";

        public string HashPassword(string password)
        {
            return string.Join("", SHA1.Create().ComputeHash(Encoding.UTF8.GetBytes(SALT + password)).Select(x => x.ToString("x2")));
        }

        public PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword)
        {
            if (hashedPassword == HashPassword(providedPassword))
                return PasswordVerificationResult.Success;
            else
                return PasswordVerificationResult.Failed;
        }
    }
}