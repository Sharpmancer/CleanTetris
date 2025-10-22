using System;
using Features.Score.Domain.Api;
using Libs.Core.Lifecycle;

namespace Features.Score.App
{
    public class ScoreDisplayPresenter : IInitializable, IDisposable
    {
        private readonly IScoreEventsDispatcher _scoreEvents;
        private readonly IScoreProvider _scoreProvider;
        private readonly IScoreDisplayView _scoreDisplay;

        public ScoreDisplayPresenter(IScoreEventsDispatcher scoreEvents, IScoreDisplayView scoreDisplay, IScoreProvider scoreProvider)
        {
            _scoreEvents = scoreEvents;
            _scoreDisplay = scoreDisplay;
            _scoreProvider = scoreProvider;
        }

        public void Initialize()
        {
            _scoreEvents.OnScoreChanged += UpdateScore;
            UpdateScore();
        }

        public void Dispose() => 
            _scoreEvents.OnScoreChanged -= UpdateScore;

        private void UpdateScore() => 
            _scoreDisplay.UpdateScore(_scoreProvider.ScorePoints);
    }
}