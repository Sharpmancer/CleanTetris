using System;
using UnityEngine;

namespace Libs.Persistence
{
    public sealed class JsonUtilitySerializer : ISerializationStrategy
    {
        public string Serialize(object obj, Type type) =>
            JsonUtility.ToJson(obj);

        public object Deserialize(string json, Type type) =>
            JsonUtility.FromJson(json, type);
    }
}