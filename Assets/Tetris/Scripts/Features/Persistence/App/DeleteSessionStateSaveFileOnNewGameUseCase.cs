using System;
using Features.MainMenu.App;
using Libs.Core;
using Libs.Persistence;

namespace Features.Persistence.App
{
    public class DeleteSessionStateSaveFileOnNewGameUseCase : DeleteSaveFileUseCase, IInitializable, IDisposable
    {
        private readonly IMainMenuEventsDispatcher _mainMenuEventsDispatcher;

        public DeleteSessionStateSaveFileOnNewGameUseCase(ISaveDeleter saveDeleter, IMainMenuEventsDispatcher mainMenuEventsDispatcher) : base(saveDeleter) => 
            _mainMenuEventsDispatcher = mainMenuEventsDispatcher;

        public void Initialize() => 
            _mainMenuEventsDispatcher.OnNewGame += DeleteSaveFile;

        public void Dispose() => 
            _mainMenuEventsDispatcher.OnNewGame -= DeleteSaveFile;
    }
}