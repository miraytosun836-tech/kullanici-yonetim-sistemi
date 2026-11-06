using System.Security.Cryptography;
using System.Text;

namespace MyProject.Helpers
{
    public static class TokenGenerator
    {
        public static string CreateToken()
        {
            byte[] bytes = RandomNumberGenerator.GetBytes(32);
            return Convert.ToHexString(bytes);
        }

        public static string HashToken(string token)
        {
            byte[] hash = SHA256.HashData(Encoding.UTF8.GetBytes(token));
            return Convert.ToHexString(hash);
        }
    }
}