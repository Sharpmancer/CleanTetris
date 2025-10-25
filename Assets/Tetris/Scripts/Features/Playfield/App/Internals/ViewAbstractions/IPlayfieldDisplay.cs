using Sharpmancer.Libs.Bitmasks;

namespace Features.Playfield.App
{
    public interface IPlayfieldDisplay
    {
        void Initialize(IReadOnlyBitMask2D mask) => 
            Initialize(mask.Columns, mask.Rows);

        void Initialize(int width, int height);
        void SetState(IReadOnlyBitMask2D state);
    }
}