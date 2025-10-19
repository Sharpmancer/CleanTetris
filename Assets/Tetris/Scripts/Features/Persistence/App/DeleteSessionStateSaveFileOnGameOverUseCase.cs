using System;
using Features.Playfield.App;
using Libs.Core.Lifecycle;
using Libs.Persistence;

namespace Features.Persistence.App
{
    public class DeleteSessionStateSaveFileOnGameOverUseCase : DeleteSaveFileUseCase, IInitializable, IDisposable
    {
        private readonly IPlayfieldEventsDispatcher _playfieldEventsDispatcher;

        public DeleteSessionStateSaveFileOnGameOverUseCase(IPlayfieldEventsDispatcher playfieldEventsDispatcher, ISaveDeleter saveDeleter) : base(saveDeleter) => 
            _playfieldEventsDispatcher = playfieldEventsDispatcher;

        public void Initialize() => 
            _playfieldEventsDispatcher.OnGameOver += DeleteSaveFile;

        public void Dispose() => 
            _playfieldEventsDispatcher.OnGameOver -= DeleteSaveFile;
    }
}