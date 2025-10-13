namespace Libs.Core
{
    public interface ISnapshotable<T>
    {
        T GetSnapshot();
        void SetSnapshot(T snapshot);
    }
}