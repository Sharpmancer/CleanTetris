using System;
using Libs.Core.Primitives;

namespace Features.Playfield.Domain.Api
{
    public interface IPlayfieldEventsDispatcher
    {
        event Action<ClearedRowsIndices> OnRowsCleared;
        event Action OnBoardStateChanged;
        event Action OnNewShapeSpawned;
        event Action OnGameOver;
    }
}