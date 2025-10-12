namespace Libs.Persistence
{
    public sealed class NoEncryption : IEncryptionStrategy
    {
        public string Encrypt(string plaintext) => 
            plaintext;
        
        public string Decrypt(string ciphertext) => 
            ciphertext;
    }
}