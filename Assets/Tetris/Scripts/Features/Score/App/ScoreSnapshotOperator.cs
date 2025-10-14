using Features.Score.Domain;
using Libs.Core;

namespace Features.Score.App
{
    public class ScoreSnapshotOperator : ISnapshotable<ScoreSnapshot>
    {
        private readonly IMementoConsumer<ScoreMemento> _mementoConsumer;
        private readonly IMementoProvider<ScoreMemento> _mementoProvider;

        public ScoreSnapshotOperator(IMementoConsumer<ScoreMemento> mementoConsumer, IMementoProvider<ScoreMemento> mementoProvider)
        {
            _mementoConsumer = mementoConsumer;
            _mementoProvider = mementoProvider;
        }

        public ScoreSnapshot GetSnapshot() => 
            ScoreSnapshot.FromMemento(_mementoProvider.GetMemento());

        public void SetSnapshot(ScoreSnapshot snapshot) => 
            _mementoConsumer.SetMemento(snapshot.ToMemento());
    }
}