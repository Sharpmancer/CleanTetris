using Features.Playfield.App;
using Libs.OneBitDisplay;
using Sharpmancer.Libs.Bitmasks;

namespace Features.Playfield.Infrastructure
{
    public class OneBitDisplayToIBoardDisplayAdapter : IPlayfieldDisplay
    {
        private readonly IOneBitDisplay _display;

        public OneBitDisplayToIBoardDisplayAdapter(IOneBitDisplay display) => 
            _display = display;

        public void Initialize(int width, int height) => 
            _display.Initialize(width, height);

        public void SetState(IReadOnlyBitMask2D state) => 
            _display.SetPixels(state.AllSetCells());
    }
}