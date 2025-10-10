using Libs.Bitmasks;

namespace Features.Gameplay.Domain
{
    public interface IBoardStateProvider
    {
        IReadOnlyBitMask2D BoardState { get; }
    }
}