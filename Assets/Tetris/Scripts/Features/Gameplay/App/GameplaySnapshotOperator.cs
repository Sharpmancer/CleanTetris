using Features.Gameplay.Domain;
using Libs.Core;

namespace Features.Gameplay.App
{
    public class GameplaySnapshotOperator : ISnapshotable<GameplaySnapshot>
    {
        private readonly IMementoProvider<GameplayMemento> _mementoProvider;
        private readonly IMementoConsumer<GameplayMemento> _mementoConsumer;

        public GameplaySnapshotOperator(IMementoProvider<GameplayMemento> mementoProvider, IMementoConsumer<GameplayMemento> mementoConsumer)
        {
            _mementoProvider = mementoProvider;
            _mementoConsumer = mementoConsumer;
        }

        public GameplaySnapshot GetSnapshot() =>
            GameplaySnapshot.FromMemento(_mementoProvider.GetMemento());

        public void SetSnapshot(GameplaySnapshot snapshot) => 
            _mementoConsumer.SetMemento(snapshot.ToMemento());
    }
}