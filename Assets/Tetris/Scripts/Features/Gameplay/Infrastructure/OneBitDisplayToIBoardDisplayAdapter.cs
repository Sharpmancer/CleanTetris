using Features.Gameplay.App;
using Libs.Bitmasks;
using Libs.OneBitDisplay;

namespace Features.Gameplay.Infrastructure
{
    public class OneBitDisplayToIBoardDisplayAdapter : IGameplayBoardDisplay
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