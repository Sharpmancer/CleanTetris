namespace Libs.Persistence
{
    public interface IEncryptionStrategy
    {
        string Encrypt(string plaintext);
        string Decrypt(string ciphertext);
    }
}