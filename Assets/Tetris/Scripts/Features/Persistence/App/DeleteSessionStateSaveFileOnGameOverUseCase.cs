using System;
using Features.Gameplay.App;
using Libs.Core.Lifecycle;
using Libs.Persistence;

namespace Features.Persistence.App
{
    public class DeleteSessionStateSaveFileOnGameOverUseCase : DeleteSaveFileUseCase, IInitializable, IDisposable
    {
        private readonly IGameplayEventsDispatcher _gameplayEventsDispatcher;

        public DeleteSessionStateSaveFileOnGameOverUseCase(IGameplayEventsDispatcher gameplayEventsDispatcher, ISaveDeleter saveDeleter) : base(saveDeleter) => 
            _gameplayEventsDispatcher = gameplayEventsDispatcher;

        public void Initialize() => 
            _gameplayEventsDispatcher.OnGameOver += DeleteSaveFile;

        public void Dispose() => 
            _gameplayEventsDispatcher.OnGameOver -= DeleteSaveFile;
    }
}