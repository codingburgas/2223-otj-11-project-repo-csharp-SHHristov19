using System.Security.Cryptography;
using System.Text;

namespace Univers.Utilities
{
    public class Utilities
    {
        /// <summary>
        /// Generator for password salt
        /// </summary>
        /// <returns></returns>
        public string GenerateSalt()
        {
            var rnd = new Random();

            var saltBytes = new byte[16];
            rnd.NextBytes(saltBytes);

            return Convert.ToHexString(saltBytes);
        }

        /// <summary>
        /// Generating a hashing bytes with the password and the salt
        /// </summary>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        public byte[] HashPasswordWithSalt(string password, string salt)
        {
            return UnicodeEncoding.UTF8.GetBytes(password + salt);
        }

        /// <summary>
        /// Hashing a password using SHA512
        /// </summary>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        public string HashPassword(string password, string salt)
        {
            var hash = SHA512.Create().ComputeHash(HashPasswordWithSalt(password, salt));

            return Convert.ToHexString(hash);
        }

        /// <summary>
        /// Generates a random 6-digit PIN code and returns it as a string.
        /// </summary>
        /// <returns>A string containing a random 6-digit PIN code.</returns>
        public string GeneratePin()
        {
            Random random = new Random();
            int pin = random.Next(100000, 999999);
            return pin.ToString();
        }
    }
}