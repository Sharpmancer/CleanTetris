using System;

namespace Features.Score.Domain
{
    public interface IScoreEventsDispatcher
    {
        int Points { get; }
        event Action<int> OnPointsAdded;
        event Action OnScoreChanged;
    }
}