using Libs.Core;

namespace Features.Score.Domain
{
    public readonly struct ScoreMemento : IMemento
    {
        public readonly int Points;
        public ScoreMemento(int points) => 
            Points = points;
    }
}