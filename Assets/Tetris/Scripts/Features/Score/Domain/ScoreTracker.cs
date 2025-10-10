using System;

namespace Features.Score.Domain
{
    public sealed class ScoreTracker : ILinesClearedHandler, IScoreEventsDispatcher
    {
        private readonly IScoreConfig _config;
        public int Points { get; private set; }
        public int Level { get; set; }
        
        public event Action<int> OnPointsAdded;
        public event Action OnScoreChanged;
        
        public ScoreTracker(IScoreConfig config) => 
            _config = config;

        public void HandleLinesCleared(int count)
        {
            var points = count switch
            {
                1 => _config.PointsForOneRow  * (Level + 1),
                2 => _config.PointsForTwoRows * (Level + 1),
                3 => _config.PointsForThreeRows * (Level + 1),
                4 => _config.PointsForFourRows * (Level + 1),
                _ => throw new ArgumentOutOfRangeException(nameof(count), count, $"{nameof(count)} must be between 0 and 4")
            };

            Points += points;
            OnPointsAdded?.Invoke(points);
            OnScoreChanged?.Invoke();
        }
    }
}