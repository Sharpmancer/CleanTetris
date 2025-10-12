namespace Libs.Persistence
{
    public interface IMigrationStrategy
    {
        void Migrate(string raw, out string migrated);
    }
}