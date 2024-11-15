namespace GosAutoAPI.IServices.Auth
{
    public interface IEncrypt
    {
        string HashPassword(string password, string salt);
        string Encrypt(string text);
        string Decrypt(string text);

    }
}
