using System;

namespace Libs.Persistence
{
    public sealed class UnixTimeMetadata : IMetadataStrategy
    {
        public string Create(object data) => 
            $"savedUtc={DateTimeOffset.UtcNow:o}";
    }
}