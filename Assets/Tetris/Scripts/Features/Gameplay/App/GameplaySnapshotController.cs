using Features.Gameplay.Domain;
using Libs.Core;

namespace Features.Gameplay.App
{
    public class GameplaySnapshotController : ISnapshotable<GameplaySnapshot>
    {
        private readonly IGameplayMementoProvider _mementoProvider;
        private readonly IGameplayMementoConsumer _mementoConsumer;

        public GameplaySnapshotController(IGameplayMementoProvider mementoProvider, IGameplayMementoConsumer mementoConsumer)
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