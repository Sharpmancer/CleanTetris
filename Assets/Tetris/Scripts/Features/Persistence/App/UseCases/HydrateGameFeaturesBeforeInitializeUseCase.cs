using Features.Shared;
using Libs.Core.Lifecycle;
using Libs.Persistence;

namespace Features.Persistence.App
{
    public class HydrateGameFeaturesBeforeInitializeUseCase : IPreInitializable
    {
        private readonly ILoader _loader;
        private readonly ISaveDataAssembleStrategy _saveDataAssembleStrategy;

        public int PreInitOrder => PreInitializationOrder.HYDRATE_GAMEPLAY_FEATURES;

        public HydrateGameFeaturesBeforeInitializeUseCase(ILoader loader, ISaveDataAssembleStrategy saveDataAssembleStrategy)
        {
            _loader = loader;
            _saveDataAssembleStrategy = saveDataAssembleStrategy;
        }

        public void PreInitialize()
        {
            if(!_loader.TryLoad(PersistenceConstants.SESSION_STATE_SAVE_KEY, _saveDataAssembleStrategy.DataAssemblyType, out var sessionState))
                return;
            _saveDataAssembleStrategy.DisassembleSaveData(sessionState);
        }
    }
}