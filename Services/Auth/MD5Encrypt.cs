using GosAutoAPI.IServices.Auth;
using Microsoft.AspNetCore.DataProtection;
using System.Security.Cryptography;

namespace GosAutoAPI.Services.Auth
{
    public class MD5Encrypt : IEncrypt //Этот способ шифрования уяизвим, поэтому будем использовать PBKDF2
    {
        private readonly IDataProtector _dataProtector;
        private const string EncryptionKey = "Key - ";

        public MD5Encrypt(IDataProtector dataProtector)
        {
            _dataProtector = dataProtector;
        }

        public string HashPassword(string password, string salt)
        {
            MD5 md5 = MD5.Create();

            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(salt + password);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            return Convert.ToHexString(hashBytes);
        }

        public string Encrypt(string input)
        {
            return _dataProtector.Protect(input);
        }

        public string Decrypt(string input)
        {
            return _dataProtector.Unprotect(input);
        }
    }
}
