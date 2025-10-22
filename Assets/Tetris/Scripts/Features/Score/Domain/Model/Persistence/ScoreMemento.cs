using Libs.Core.Patterns.Memento;

namespace Features.Score.Domain.Model
{
    public readonly struct ScoreMemento : IMemento
    {
        public readonly int Points;
        public ScoreMemento(int points) => 
            Points = points;
    }
}