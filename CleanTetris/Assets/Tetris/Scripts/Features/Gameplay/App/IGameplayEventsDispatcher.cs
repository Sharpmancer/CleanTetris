using System;
using Libs.Core;

namespace Features.Gameplay.App
{
    public interface IGameplayEventsDispatcher
    {
        event Action<UpToFourBytes> OnRowsCleared;
    }
}