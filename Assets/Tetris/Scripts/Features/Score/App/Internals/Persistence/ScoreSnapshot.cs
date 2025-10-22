using System;
using Features.Score.Domain.Model;
using Libs.Core.Patterns.Snapshot;

namespace Features.Score.App.Internals
{
    [Serializable]
    public struct ScoreSnapshot : ISnapshot<ScoreMemento>
    {
        public int Points;
        
        public ISnapshot<ScoreMemento> Hydrate(ScoreMemento memento)
        {
            Points = memento.Points;
            return this;
        }

        public ScoreMemento ToMemento() => 
            new(Points);
    }
}