using Libs.Bitmasks;

namespace Features.Gameplay.App
{
    public interface IGameplayBoardDisplay
    {
        void Initialize(IReadOnlyBitMask2D mask) => 
            Initialize(mask.Columns, mask.Rows);

        void Initialize(int width, int height);
        void SetState(IReadOnlyBitMask2D state);
    }
}