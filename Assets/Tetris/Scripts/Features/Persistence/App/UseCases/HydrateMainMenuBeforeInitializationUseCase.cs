using Features.MainMenu.App;
using Features.Shared;
using Libs.Core.Lifecycle;
using Libs.Persistence;

namespace Features.Persistence.App
{
    public class HydrateMainMenuBeforeInitializationUseCase : IPreInitializable
    {
        private readonly ILoader _loader;
        private readonly ISaveDataAssemblyTypeProvider _saveDataTypeProvider;
        private readonly IContinueGameIsAvailableHydratable _continueGameIsAvailableHydratable;
        
        public int PreInitOrder => PreInitializationOrder.HYDRATE_MAIN_MENU_FEATURES;

        public HydrateMainMenuBeforeInitializationUseCase(
            ILoader loader, 
            IContinueGameIsAvailableHydratable continueGameIsAvailableHydratable, 
            ISaveDataAssemblyTypeProvider saveDataTypeProvider)
        {
            _loader = loader;
            _continueGameIsAvailableHydratable = continueGameIsAvailableHydratable;
            _saveDataTypeProvider = saveDataTypeProvider;
        }

        public void PreInitialize()
        {
            // trying to load concrete type to make sure file is valid
            var saveExists = _loader.TryLoad(PersistenceConstants.SESSION_STATE_SAVE_KEY, _saveDataTypeProvider.DataAssemblyType, out _);
            _continueGameIsAvailableHydratable.SetContinueGameIsAvailable(saveExists);
        }
    }
}