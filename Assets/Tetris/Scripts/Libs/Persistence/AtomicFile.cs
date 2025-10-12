using System;
using System.IO;
using System.Text;

namespace Libs.Persistence
{
    /// <summary>
    /// Provides safe, atomic file write and read operations.
    /// Writes go through a temporary file which is then renamed into place.
    /// </summary>
    public static class AtomicFile
    {
        public static void WriteAllText(string path, string contents, Encoding encoding = null)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentException("Path is null or whitespace.", nameof(path));

            var dir = Path.GetDirectoryName(path);
            if (string.IsNullOrEmpty(dir))
                throw new InvalidOperationException($"Invalid directory for path: {path}");

            Directory.CreateDirectory(dir);

            // Write to a temporary file in the same directory.
            var tempPath = path + ".tmp";

            encoding ??= Encoding.UTF8;
            using (var fs = new FileStream(tempPath, FileMode.Create, FileAccess.Write, FileShare.None))
            using (var writer = new StreamWriter(fs, encoding))
            {
                writer.Write(contents);
                writer.Flush();
                fs.Flush(true);
            }

            if (!File.Exists(path))
            {
                File.Move(tempPath, path);
                return;
            }

            var backup = path + ".bak";
            try
            {
                File.Replace(tempPath, path, backup, ignoreMetadataErrors: true);
                File.Delete(backup);
            }
            catch (PlatformNotSupportedException)
            {
                // Some platforms (e.g., Android) lack File.Replace; fallback to delete+move.
                File.Delete(path);
                File.Move(tempPath, path);
            }
        }

        /// <summary>
        /// Reads text safely (normal File.ReadAllText but validates existence).
        /// </summary>
        public static bool TryReadAllText(string path, out string contents, Encoding encoding = null)
        {
            contents = null;
            if (!File.Exists(path))
                return false;

            encoding ??= Encoding.UTF8;
            try
            {
                contents = File.ReadAllText(path, encoding);
                return true;
            }
            catch
            {
                contents = null;
                return false;
            }
        }
    }
}
