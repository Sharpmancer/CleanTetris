using System;

namespace Libs.Core.Patterns.Snapshot
{
    public interface ISnapshotable
    {
        Type SnapshotType { get; }
        object GetSnapshot();
        void SetSnapshot(object snapshot);
    }

    public interface ISnapshotable<T> where T : ISnapshot
    {
        T GetSnapshot();
        void SetSnapshot(T snapshot);
    }
}