using Features.MainMenu.App;
using Features.Persistence.App;
using Libs.Bootstrap;
using Libs.Persistence;

namespace Features.Persistence.Composition
{
    public class MainMenuPersistenceInstaller : Installer
    {
        public override void Install(IInstallableContext context)
        {
            ISaveDataAssemblyTypeProvider saveDataAssemblyStrategyAsOnlyTypeProvider =
                new ManualSaveDataAssembleStrategy(gameplaySnapshotable: null, scoreSnapshotable: null);
            
            var deleteSaveOnNewGameUseCase = new DeleteSessionStateSaveFileOnNewGameUseCase(
                context.Get<ISaveDeleter>(), 
                context.Get<IMainMenuEventsDispatcher>());

            var hydrateMainMenuUseCase = new HydrateMainMenuBeforeInitializationUseCase(
                context.Get<ILoader>(), 
                context.Get<IContinueGameIsAvailableHydratable>(),
                saveDataTypeProvider: saveDataAssemblyStrategyAsOnlyTypeProvider);
            
            context.RegisterRunnable(deleteSaveOnNewGameUseCase);
            context.RegisterRunnable(hydrateMainMenuUseCase);
        }
    }
}