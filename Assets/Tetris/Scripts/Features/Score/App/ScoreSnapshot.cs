using System;
using Features.Score.Domain;

namespace Features.Score.App
{
    [Serializable]
    public record ScoreSnapshot
    {
        public int Points;
        
        internal static ScoreSnapshot FromMemento(ScoreMemento memento) => 
            new() { Points = memento.Points };
        
        internal ScoreMemento ToMemento() => 
            new(Points);
    }
}