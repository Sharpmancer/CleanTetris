using System;

namespace Libs.Persistence
{
    [Serializable]
    public sealed class Envelope
    {
        public string EncryptionMeta;
        public string CipherText;

        public Envelope(string encryptionMeta, string cipherText)
        {
            EncryptionMeta = encryptionMeta;
            CipherText = cipherText;
        }
    }

    [Serializable]
    public sealed class Payload
    {
        public int Version;
        public string Data;

        public Payload(int version, string data)
        {
            Version = version;
            Data = data;
        }
    }
}