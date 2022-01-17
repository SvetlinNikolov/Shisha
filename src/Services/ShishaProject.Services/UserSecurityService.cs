namespace ShishaProject.Services
{
    using System.Security.Cryptography;
    using System.Text;

    using BCrypt.Net;
    using ShishaProject.Services.Interfaces;

    public class UserSecurityService : IUserSecurityService
    {
        public string EncryptPassword(string inputString)
        {
            SHA512 sha512 = SHA512.Create();

            byte[] bytes = Encoding.UTF8.GetBytes(inputString);
            byte[] hash = sha512.ComputeHash(bytes);

            return this.GetStringFromHash(hash);
        }

        private string GetStringFromHash(byte[] hash)
        {
            StringBuilder result = new();

            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2"));
            }

            return result.ToString();
        }
    }
}
