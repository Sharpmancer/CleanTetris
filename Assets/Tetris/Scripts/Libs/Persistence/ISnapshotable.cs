using System;

namespace Libs.Persistence
{
    public interface ISnapshotable
    {
        Type SnapshotType { get; }
        object GetSnapshot();
        void SetSnapshot(object snapshot);
    }
}