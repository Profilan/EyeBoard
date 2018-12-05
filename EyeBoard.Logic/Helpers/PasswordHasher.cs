using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace EyeBoard.Logic.Helpers
{
    public class PasswordHasher : IPasswordHasher
    {
        const string SALT = "%$#HD&^5637*";

        public string HashPassword(string password)
        {
            return string.Join("", SHA1CryptoServiceProvider.Create().ComputeHash(Encoding.UTF8.GetBytes(SALT + password)).Select(x => x.ToString("x2")));
        }
    }
}
