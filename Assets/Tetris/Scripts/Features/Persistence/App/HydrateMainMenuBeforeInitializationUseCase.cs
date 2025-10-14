using Features.MainMenu.App;
using Features.Shared;
using Libs.Core;
using Libs.Persistence;

namespace Features.Persistence.App
{
    public class HydrateMainMenuBeforeInitializationUseCase : IPreInitializable
    {
        private readonly ILoader _loader;
        private readonly IContinueGameIsAvailableHydratable _continueGameIsAvailableHydratable;
        public int Order => PreInitializationOrder.HYDRATE_MAIN_MENU_FEATURES;

        public HydrateMainMenuBeforeInitializationUseCase(IContinueGameIsAvailableHydratable continueGameIsAvailableHydratable, ILoader loader)
        {
            _continueGameIsAvailableHydratable = continueGameIsAvailableHydratable;
            _loader = loader;
        }

        public void PreInitialize()
        {
            var saveExists = _loader.TryLoad(PersistenceConstants.SESSION_STATE_SAVE_KEY, typeof(SessionStateDataAssembly), out _);
            _continueGameIsAvailableHydratable.SetContinueGameIsAvailable(saveExists);
        }
    }
}