using System;

namespace Features.Playfield.App
{
    public interface IPlayfieldEventsDispatcher
    {
        event Action<int> OnRowsCleared;
        event Action OnBoardStateChanged;
        event Action OnGameOver;
    }
}