using Features.Gameplay.App;
using Features.Persistence.App;
using Features.Score.App;
using Libs.Bootstrap;
using Libs.Core;
using Libs.Persistence;

namespace Features.Persistence.Composition
{
    public class GameplayPersistenceInstaller : Installer
    {
        public override void Install(IInstallableContext context)
        {
            var saveUseCase = new SaveOnGameStateChangedUseCase(
                context.Get<IGameplayEventsDispatcher>(), 
                context.Get<ISaver>(), 
                context.Get<ISnapshotable<GameplaySnapshot>>(), 
                context.Get<ISnapshotable<ScoreSnapshot>>());

            var loadUseCase = new HydrateGameFeaturesBeforeInitializeUseCase(
                context.Get<ILoader>(),
                context.Get<ISnapshotable<GameplaySnapshot>>(),
                context.Get<ISnapshotable<ScoreSnapshot>>());
            
            var deleteUseCase = new DeleteSessionStateSaveFileOnGameOverUseCase(
                context.Get<IGameplayEventsDispatcher>(), 
                context.Get<ISaveDeleter>());
            
            context.RegisterRunnable(saveUseCase);
            context.RegisterRunnable(loadUseCase);
            context.RegisterRunnable(deleteUseCase);
        }
    }
}