namespace Libs.Persistence
{
    public interface IMigrationStrategy
    {
        MigrationResult TryMigrate(string rawData, int dataVersion, int targetVersion, out string migrated);
    }
}