using Libs.Bitmasks;

namespace Features.Playfield.Domain
{
    public interface IPlayfieldStateProvider
    {
        IReadOnlyBitMask2D BoardState { get; }
    }
}