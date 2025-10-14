# Persistence — simple, pluggable save/load for Unity

Safe, composable persistence for Unity projects.  
Serialize → (optionally) encrypt → wrap with metadata → **atomically** write to disk.  
Load does the inverse, with an optional **migration** step to keep old saves compatible.

- **Atomic writes** (no corrupted saves on crash)
- **Strategy-based design** (swap serialization, encryption, file ops, metadata, migration)
- **Unity-ready** (defaults to `Application.persistentDataPath/Saves`)
- **Snapshot pattern** via `ISnapshotable` (clean separation of domain vs. data)

---

## Contents

| Component | Description |
|------------|-------------|
| `PersistentDataHandler` | Main facade implementing `ISaver`, `ILoader`, `ISaveDeleter`. Orchestrates the pipeline. |
| `ISerializationStrategy` | Pluggable serializer (`Serialize/Deserialize`). Default: `JsonUtilitySerializer`. |
| `IEncryptionStrategy` | Pluggable encryption (`Encrypt/Decrypt`). Default: `NoEncryption`. |
| `IMetadataStrategy` | Adds save metadata (timestamps, version, etc.). Default: `UnixTimeMetadata`. |
| `IMigrationStrategy` | Upgrades raw stored text before parsing (back-compat). Default: `NoMigration`. |
| `IFileOperationsStrategy` | Abstracts disk I/O. Default: `PersistentDataPathFileOperationsStrategy`. |
| `AtomicFile` | Helper providing **atomic** `WriteAllText` and `TryReadAllText`. |
| `ISnapshotable` | Domain helper for snapshot in/out with a separate DTO. |
| `Envelope` | Internal `{ Meta: string, Payload: string }` container for metadata + encrypted payload. |

---

[← Back](../../../../../README.md)