using System;
using Libs.Core;

namespace Features.Gameplay.Domain
{
    public interface IGameplayEvents
    {
        event Action<UpToFourBytes> OnRowsCleared;
        event Action OnBoardStateChanged;
        event Action OnNewShapeSpawned;
        event Action OnGameOver;
    }
}