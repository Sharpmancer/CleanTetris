using System;

namespace Libs.Persistence
{
    public sealed class NoEncryption : IEncryptionStrategy
    {
        private const string META = "no encryption";

        public string Encrypt(string plaintext, out string meta)
        {
            meta = "no encryption";
            return plaintext;
        }

        public string Decrypt(string meta, string ciphertext)
        {
            if (meta != META)
                throw new InvalidOperationException($"Invalid meta value: {meta}, expected: {META}");
            return ciphertext;
        }
    }
}