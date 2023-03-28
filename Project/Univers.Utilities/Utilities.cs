using System.Text;

namespace UniversUtilities
{
    public class Utilities
    {
        public string GenerateSalt()
        {
            var rnd = new Random();

            var saltBytes = new byte[16];
            rnd.NextBytes(saltBytes);

            return Convert.ToHexString(saltBytes);
        }

        public byte[] HashPasswordWithSalt(string password, string salt)
        {
            return UnicodeEncoding.UTF8.GetBytes(password + salt);
        }
    }
}