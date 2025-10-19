namespace Libs.Persistence
{
    public interface IEncryptionStrategy
    {
        string Encrypt(string plaintext, out string meta);
        string Decrypt(string meta, string ciphertext);
    }
}