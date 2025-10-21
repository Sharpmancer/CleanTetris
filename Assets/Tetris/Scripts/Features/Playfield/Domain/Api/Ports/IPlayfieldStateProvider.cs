using Libs.Bitmasks;

namespace Features.Playfield.Domain.Api
{
    public interface IPlayfieldStateProvider
    {
        // TODO: rename to fit the domain terms
        IReadOnlyBitMask2D BoardState { get; }
    }
}