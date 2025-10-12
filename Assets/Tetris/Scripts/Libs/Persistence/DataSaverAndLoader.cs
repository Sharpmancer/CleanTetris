using System;

namespace Libs.Persistence
{
    public sealed class DataSaverAndLoader : ISaver, ILoader
    {
        private readonly ISerializationStrategy _serializer;
        private readonly IEncryptionStrategy _encryptor;
        private readonly IMetadataStrategy _metadata;
        private readonly IMigrationStrategy _migrator;
        private readonly IWritingStrategy _writer;

        public DataSaverAndLoader(
            ISerializationStrategy serializer = null,
            IEncryptionStrategy encryptor = null,
            IMetadataStrategy metadata = null,
            IMigrationStrategy migrator = null,
            IWritingStrategy writer = null)
        {
            _serializer = serializer ?? new JsonUtilitySerializer();
            _encryptor = encryptor ?? new NoEncryption();
            _metadata = metadata ?? new UnixTimeMetadata();
            _migrator = migrator ?? new NoMigration();
            _writer = writer ?? new PersistentDataPathFileWriter();
        }

        public void Save(string key, object data)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Key is null or whitespace.", nameof(key));
            if (data is null)
                throw new ArgumentNullException(nameof(data));

            var payloadJson = _serializer.Serialize(data, data.GetType());
            var encrypted = _encryptor.Encrypt(payloadJson);
            var meta = _metadata.Create(data);
            var envelope = new Envelope { Meta = meta, Payload = encrypted };
            var envJson = _serializer.Serialize(envelope, envelope.GetType());
            _writer.Write(key, envJson);
        }

        public bool TryLoad(string key, Type snapshotType, out object data)
        {
            data = null;
            if (string.IsNullOrWhiteSpace(key) || snapshotType is null)
                return false;

            if (!_writer.TryRead(key, out var raw))
                return false;

            _migrator.Migrate(raw, out var migrated);

            try
            {
                var envelope = (Envelope)_serializer.Deserialize(migrated, typeof(Envelope));
                var decrypted = _encryptor.Decrypt(envelope.Payload ?? string.Empty);
                data = _serializer.Deserialize(decrypted, snapshotType);
            }
            catch
            {
                data = null;
            }
            
            return data != null;
        }
    }
}