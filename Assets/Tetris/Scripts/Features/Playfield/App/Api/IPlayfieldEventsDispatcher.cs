using System;
using Libs.Core.Primitives;

namespace Features.Playfield.App
{
    public interface IPlayfieldEventsDispatcher
    {
        event Action<UpToFourBytes> OnRowsCleared;
        event Action OnBoardStateChanged;
        event Action OnGameOver;
    }
}