using System;
using Features.Score.Domain;
using Libs.Core;

namespace Features.Score.App
{
    public class ScoreDisplayPresenter : IInitializable, IDisposable
    {
        private readonly IScoreEventsDispatcher _scoreEvents;
        private readonly IScoreDisplayView _scoreDisplay;

        public ScoreDisplayPresenter(IScoreEventsDispatcher scoreEvents, IScoreDisplayView scoreDisplay)
        {
            _scoreEvents = scoreEvents;
            _scoreDisplay = scoreDisplay;
        }

        public void Initialize()
        {
            _scoreEvents.OnScoreChanged += UpdateScore;
            UpdateScore();
        }

        public void Dispose() => 
            _scoreEvents.OnScoreChanged -= UpdateScore;

        private void UpdateScore() => 
            _scoreDisplay.UpdateScore(_scoreEvents.Points);
    }
}