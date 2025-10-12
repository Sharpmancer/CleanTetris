using System;

namespace Libs.Persistence
{
    public interface ILoader
    {
        bool TryLoad(string key, Type snapshotType, out object data);
    }
}