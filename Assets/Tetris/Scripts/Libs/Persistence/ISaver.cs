namespace Libs.Persistence
{
    public interface ISaver
    {
        void Save(string key, object data);
    }
}