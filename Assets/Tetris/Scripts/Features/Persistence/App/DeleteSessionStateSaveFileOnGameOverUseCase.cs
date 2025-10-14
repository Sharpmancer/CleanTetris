using System;
using Features.Gameplay.App;
using Libs.Core;
using Libs.Persistence;

namespace Features.Persistence.App
{
    public class DeleteSessionStateSaveFileOnGameOverUseCase : IInitializable, IDisposable
    {
        private readonly ISaveDeleter _saveDeleter;
        private readonly IGameplayEventsDispatcher _gameplayEventsDispatcher;

        public DeleteSessionStateSaveFileOnGameOverUseCase(IGameplayEventsDispatcher gameplayEventsDispatcher, ISaveDeleter saveDeleter)
        {
            _gameplayEventsDispatcher = gameplayEventsDispatcher;
            _saveDeleter = saveDeleter;
        }

        public void Initialize() => 
            _gameplayEventsDispatcher.OnGameOver += RemoveSaveFile;

        public void Dispose() => 
            _gameplayEventsDispatcher.OnGameOver -= RemoveSaveFile;

        private void RemoveSaveFile() => 
            _saveDeleter.Delete(PersistenceConstants.SAVE_KEY);
    }
}