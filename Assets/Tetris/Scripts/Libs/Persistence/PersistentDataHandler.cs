using System;

namespace Libs.Persistence
{
    public sealed class PersistentDataHandler : ISaver, ILoader, ISaveDeleter
    {
        private readonly ISerializationStrategy _serializer;
        private readonly IEncryptionStrategy _encryptor;
        private readonly IMigrationStrategy _migrator;
        private readonly IFileOperationsStrategy _fileOps;
        private readonly IPayloadVersionProvider _versionProvider;

        public PersistentDataHandler(ISerializationStrategy serializer = null, IEncryptionStrategy encryptor = null,
            IMigrationStrategy migrator = null,
            IFileOperationsStrategy _fileOps = null,
            IPayloadVersionProvider versionProvider = null)
        {
            _serializer = serializer ?? new JsonUtilitySerializer();
            _encryptor = encryptor ?? new NoEncryption();
            _migrator = migrator ?? new NoMigration();
            this._fileOps = _fileOps ?? new PersistentDataPathFileOperationsStrategy();
            _versionProvider = versionProvider ?? new StaticPayloadVersionProvider(0);
        }

        public void Save(string key, object data)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Key is null or whitespace.", nameof(key));
            if (data is null)
                throw new ArgumentNullException(nameof(data));

            var serializedData = _serializer.Serialize(data, data.GetType());
            var payload = new Payload(_versionProvider.Version, serializedData);
            var serializedPayload = _serializer.Serialize(payload, typeof(Payload));
            var encryptedPayload = _encryptor.Encrypt(serializedPayload, out var encryptionMeta);
            var envelope = new Envelope(encryptionMeta: encryptionMeta, cipherText: encryptedPayload);
            var serializedEnvelope = _serializer.Serialize(envelope, envelope.GetType());
            _fileOps.Write(key, serializedEnvelope);
        }

        public bool TryLoad(string key, Type dataType, out object data)
        {
            data = null;
            if (string.IsNullOrWhiteSpace(key) || dataType is null)
                return false;

            if (!_fileOps.TryRead(key, out var raw))
                return false;

            try
            {
                var envelope = (Envelope)_serializer.Deserialize(raw, typeof(Envelope));
                var decryptedPayloadString = _encryptor.Decrypt(meta: envelope.EncryptionMeta, ciphertext: envelope.CipherText);
                var payload = (Payload)_serializer.Deserialize(decryptedPayloadString, typeof(Payload));
                var migrationResult = _migrator.TryMigrate(rawData: payload.Data, dataVersion: payload.Version, targetVersion: _versionProvider.Version, out var migratedData);
                switch (migrationResult)
                {
                    case MigrationResult.NoneNeeded:
                        data = _serializer.Deserialize(payload.Data, dataType);
                        break;
                    case MigrationResult.Success:
                        data = _serializer.Deserialize(migratedData, dataType); 
                        Save(key, data);
                        break;
                    case MigrationResult.Failed:
                        throw new Exception($"migration {payload.Version}->{_versionProvider.Version} failed");
                    default:
                        throw new ArgumentOutOfRangeException(nameof(migrationResult), migrationResult, null);
                }
            }
            catch
            {
                data = null;
            }
            
            return data != null;
        }

        public void Delete(string key) => 
            _fileOps.Delete(key);
    }
}