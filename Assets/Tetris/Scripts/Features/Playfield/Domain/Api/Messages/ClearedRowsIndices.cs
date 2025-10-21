using Libs.Core.Primitives;

namespace Features.Playfield.Domain.Api
{
    public readonly struct ClearedRowsIndices
    {
        private readonly UpToFourBytes _indices;

        public byte this[int index] => 
            _indices[index];

        public int Count => _indices.Count;

        public ClearedRowsIndices(UpToFourBytes indices) => 
            _indices = indices;
    }
}