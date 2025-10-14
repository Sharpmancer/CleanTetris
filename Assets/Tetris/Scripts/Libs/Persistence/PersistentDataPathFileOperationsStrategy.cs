using System.IO;
using UnityEngine;

namespace Libs.Persistence
{
    public sealed class PersistentDataPathFileOperationsStrategy : IFileOperationsStrategy
    {
        private readonly string _dir;

        public PersistentDataPathFileOperationsStrategy(string subDirectory = "Saves")
        {
            _dir = string.IsNullOrWhiteSpace(subDirectory)
                ? Application.persistentDataPath
                : Path.Combine(Application.persistentDataPath, subDirectory);
            Directory.CreateDirectory(_dir);
        }

        public void Write(string key, string data)
        {
            var path = PathFor(key);
            AtomicFile.WriteAllText(path, data);
        }

        public bool TryRead(string key, out string data)
        {
            data = null;
            var path = PathFor(key);
            if(!File.Exists(path))
                return false;
            if(!AtomicFile.TryReadAllText(path, out data))
                return false;
            
            return !string.IsNullOrEmpty(data);
        }

        public void Delete(string key) => 
            File.Delete(PathFor(key));

        private string PathFor(string key)
        {
            foreach (var c in Path.GetInvalidFileNameChars())
                key = key.Replace(c, '_');
            return Path.Combine(_dir, key + ".json");
        }
    }
}