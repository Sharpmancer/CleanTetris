namespace Libs.Persistence
{
    public interface IWritingStrategy
    {
        void Write(string key, string data);
        bool TryRead(string key, out string data);
    }
}