using System;
using Libs.Core;
using Libs.Core.Primitives;

namespace Features.Gameplay.Domain
{
    public interface IGameplayEventsDispatcher
    {
        event Action<UpToFourBytes> OnRowsCleared;
        event Action OnBoardStateChanged;
        event Action OnNewShapeSpawned;
        event Action OnGameOver;
    }
}