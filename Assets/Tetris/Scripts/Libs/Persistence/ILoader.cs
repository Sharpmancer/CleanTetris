using System;

namespace Libs.Persistence
{
    public interface ILoader
    {
        bool TryLoad(string key, Type dataType, out object data);
    }
}