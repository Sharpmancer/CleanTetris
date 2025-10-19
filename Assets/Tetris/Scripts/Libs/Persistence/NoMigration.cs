namespace Libs.Persistence
{
    public sealed class NoMigration : IMigrationStrategy
    {
        public MigrationResult TryMigrate(string rawData, int dataVersion, int targetVersion, out string migrated)
        {
            migrated = null;
            if(dataVersion != targetVersion)
                throw new System.Exception("DataVersion mismatch. No migration strategy assumes no version change either");
            return MigrationResult.NoneNeeded;
        }
    }
}