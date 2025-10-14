using Features.Gameplay.App;
using Features.Score.App;
using Features.Shared;
using Libs.Core;
using Libs.Persistence;

namespace Features.Persistence.App
{
    public class HydrateGameFeaturesBeforeInitializeUseCase : IPreInitializable
    {
        private readonly ILoader _loader;
        private readonly ISnapshotable<GameplaySnapshot> _gameplaySnapshot;
        private readonly ISnapshotable<ScoreSnapshot> _scoreSnapshot;

        public int Order => PreInitializationOrder.HYDRATE_GAMEPLAY_FEATURES;

        public HydrateGameFeaturesBeforeInitializeUseCase(ILoader loader, ISnapshotable<GameplaySnapshot> gameplaySnapshot, ISnapshotable<ScoreSnapshot> scoreSnapshot)
        {
            _loader = loader;
            _gameplaySnapshot = gameplaySnapshot;
            _scoreSnapshot = scoreSnapshot;
        }

        public void PreInitialize()
        {
            if(!_loader.TryLoad(PersistenceConstants.SAVE_KEY, typeof(SaveDataAssembly), out var sessionState))
                return;
            var data = (SaveDataAssembly)sessionState;
            _gameplaySnapshot.SetSnapshot(data.GameplaySnapshot);
            _scoreSnapshot.SetSnapshot(data.ScoreSnapshot);
        }
    }
}