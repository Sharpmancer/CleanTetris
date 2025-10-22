using System;
using Features.Score.Domain.Api;

namespace Features.Score.Domain.Model
{
    internal sealed class Score : ILinesClearedHandler, IScoreEventsDispatcher, IScoreProvider, IScorePersistencePort
    {
        private readonly IPointsPerRowsClearedCalculationStrategy _pointsCalculationStrategy;
        public int ScorePoints { get; private set; }
        public event Action OnScoreChanged;
        
        internal Score(IPointsPerRowsClearedCalculationStrategy pointsCalculationStrategy) => 
            _pointsCalculationStrategy = pointsCalculationStrategy;

        public void HandleLinesCleared(int count)
        {
            var points = _pointsCalculationStrategy.GetPoints(count);
            ScorePoints += points;
            OnScoreChanged?.Invoke();
        }

        public ScoreMemento GetMemento() => 
            new(ScorePoints);

        public void SetMemento(ScoreMemento Memento)
        {
            ScorePoints = Memento.Points;
            OnScoreChanged?.Invoke();
        }
    }
}