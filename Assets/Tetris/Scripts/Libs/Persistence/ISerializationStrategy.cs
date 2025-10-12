using System;

namespace Libs.Persistence
{
    public interface ISerializationStrategy
    {
        string Serialize(object obj, Type type);
        object Deserialize(string json, Type type);
    }
}
