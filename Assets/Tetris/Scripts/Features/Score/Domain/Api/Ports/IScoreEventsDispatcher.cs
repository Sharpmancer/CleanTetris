using System;

namespace Features.Score.Domain.Api
{
    public interface IScoreEventsDispatcher
    {
        event Action OnScoreChanged;
    }
}