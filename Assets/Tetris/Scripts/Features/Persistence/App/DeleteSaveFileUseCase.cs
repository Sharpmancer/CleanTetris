using Libs.Persistence;

namespace Features.Persistence.App
{
    public abstract class DeleteSaveFileUseCase
    {
        private readonly ISaveDeleter _saveDeleter;

        protected DeleteSaveFileUseCase(ISaveDeleter saveDeleter) => 
            _saveDeleter = saveDeleter;

        protected void DeleteSaveFile() => 
            _saveDeleter.Delete(PersistenceConstants.SESSION_STATE_SAVE_KEY);
    }
}