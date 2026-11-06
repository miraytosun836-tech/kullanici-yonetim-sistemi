using System.Security.Cryptography;

namespace MyProject.Helpers
{
    public sealed class PasswordHashResult
    {
        public string Hash { get; init; } = "";
        public string Salt { get; init; } = "";
        public int Iterations { get; init; }
    }

    public class PasswordHasher
    {
        private const int Iterations = 210000;
        private const int SaltSize = 16;
        private const int HashSize = 32;

        public PasswordHashResult HashPassword(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
                password,
                salt,
                Iterations,
                HashAlgorithmName.SHA256,
                HashSize);

            return new PasswordHashResult
            {
                Hash = Convert.ToBase64String(hash),
                Salt = Convert.ToBase64String(salt),
                Iterations = Iterations
            };
        }

        public bool Verify(string password, string storedHash, string storedSalt, int iterations)
        {
            byte[] salt = Convert.FromBase64String(storedSalt);
            byte[] actual = Rfc2898DeriveBytes.Pbkdf2(
                password,
                salt,
                iterations,
                HashAlgorithmName.SHA256,
                HashSize);

            return CryptographicOperations.FixedTimeEquals(
                actual,
                Convert.FromBase64String(storedHash));
        }
    }
}
