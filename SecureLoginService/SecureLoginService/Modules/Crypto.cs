using System;
using System.Security.Cryptography;
using System.Text;
using BCrypt.Net;

namespace SecureLoginService.Modules
{
    public static class Crypto
    {
        public static string SHA512Encrypt(string text)
        {
            byte[] hash;
            using (SHA512 sha = new SHA512Managed())
            {
                hash = sha.ComputeHash(Encoding.UTF8.GetBytes(text));

            }
            return GetStringFromHash(hash);
        }

        static string GetStringFromHash(byte[] hash)
        {
            StringBuilder result = new StringBuilder();
            foreach (var x in hash)
            {
                result.Append(x.ToString("X2"));
            }
            return result.ToString();
        }

        public static string BCryptEncrypt(string text)
        {
            string salt = BCrypt.Net.BCrypt.GenerateSalt(12);
            string hash = BCrypt.Net.BCrypt.HashPassword(SHA512Encrypt(text), salt);
            return hash;
        }

        public static bool Validate(string submittedPassword, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(SHA512Encrypt(submittedPassword), hashedPassword);
        }

    }
}
