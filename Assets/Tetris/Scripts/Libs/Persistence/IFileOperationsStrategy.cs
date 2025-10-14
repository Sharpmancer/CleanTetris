namespace Libs.Persistence
{
    public interface IFileOperationsStrategy
    {
        void Write(string key, string data);
        bool TryRead(string key, out string data);
        void Delete(string key);
    }
}