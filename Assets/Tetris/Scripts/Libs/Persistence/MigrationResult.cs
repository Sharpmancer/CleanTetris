namespace Libs.Persistence
{
    public enum MigrationResult
    {
        // guarding against default value being treated as an explicitly set one
        None = 0,
        NoneNeeded = 1,
        Success = 2,   
        Failed = 3,     
    }
}