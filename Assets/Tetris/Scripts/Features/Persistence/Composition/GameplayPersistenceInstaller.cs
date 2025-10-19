using Features.Persistence.App;
using Features.Playfield.App;
using Features.Score.App;
using Libs.Bootstrap;
using Libs.Core.Patterns.Snapshot;
using Libs.Persistence;

namespace Features.Persistence.Composition
{
    public class GameplayPersistenceInstaller : Installer
    {
        public override void Install(IInstallableContext context)
        {
            var saveDataAssembleStrategy = new ManualSaveDataAssembleStrategy(
                context.Get<ISnapshotable<PlayfieldSnapshot>>(), 
                context.Get<ISnapshotable<ScoreSnapshot>>());
            
            var saveUseCase = new SaveOnGameBoardStateChangedUseCase(
                context.Get<IPlayfieldEventsDispatcher>(), 
                context.Get<ISaver>(), 
                saveDataAssembleStrategy);

            var loadUseCase = new HydrateGameFeaturesBeforeInitializeUseCase(
                context.Get<ILoader>(),
                saveDataAssembleStrategy);
            
            var deleteUseCase = new DeleteSessionStateSaveFileOnGameOverUseCase(
                context.Get<IPlayfieldEventsDispatcher>(), 
                context.Get<ISaveDeleter>());
            
            context.RegisterRunnable(saveUseCase);
            context.RegisterRunnable(loadUseCase);
            context.RegisterRunnable(deleteUseCase);
        }
    }
}