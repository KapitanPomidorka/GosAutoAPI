using GosAutoAPI.IServices.Auth;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.DataProtection;

namespace GosAutoAPI.Services.Auth
{
    public class PBKDF2Encrypt : IEncrypt
    {

        private readonly IDataProtector _dataProtector;
        private const string EncryptionKey = "лун!!!";

        public PBKDF2Encrypt(IDataProtectionProvider dataProtectionProvider)
        {
            _dataProtector = dataProtectionProvider.CreateProtector(EncryptionKey);
        }

        public string HashPassword(string password, string salt)
        {
            byte[] saltBytes = System.Text.Encoding.ASCII.GetBytes(salt);

            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: saltBytes,
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: 100000,
                numBytesRequested: 512 / 8));
        }

        public string Encrypt(string text)
        {
            return _dataProtector.Protect(text);
        }

        public string Decrypt(string text)
        {
            return _dataProtector.Unprotect(text);
        }
    }
}
