namespace Libs.Persistence
{
    public sealed class NoMigration : IMigrationStrategy
    {
        public void Migrate(string raw, out string migrated) => 
            migrated = raw;
    }
}