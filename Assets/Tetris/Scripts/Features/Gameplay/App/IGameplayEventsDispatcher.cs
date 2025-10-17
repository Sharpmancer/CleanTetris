using System;
using Libs.Core.Primitives;

namespace Features.Gameplay.App
{
    public interface IGameplayEventsDispatcher
    {
        event Action<UpToFourBytes> OnRowsCleared;
        event Action OnBoardStateChanged;
        event Action OnGameOver;
    }
}