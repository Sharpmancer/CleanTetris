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
            var deleteSaveOnNewGameUseCase = new DeleteSessionStateSaveFileOnNewGameUseCase(
                context.Get<ISaveDeleter>(), 
                context.Get<IMainMenuEventsDispatcher>());

            var hydrateMainMenuUseCase = new HydrateMainMenuBeforeInitializationUseCase(
                context.Get<IContinueGameIsAvailableHydratable>(),
                context.Get<ILoader>());
            
            context.RegisterRunnable(deleteSaveOnNewGameUseCase);
            context.RegisterRunnable(hydrateMainMenuUseCase);
        }
    }
}