using System;
using Features.Gameplay.App;
using Libs.Core.Lifecycle;
using Libs.Persistence;

namespace Features.Persistence.App
{
    public class SaveOnGameBoardStateChangedUseCase : IInitializable, IDisposable
    {
        private readonly ISaver _saver;
        private readonly IGameplayEventsDispatcher _gameEvents;
        private readonly ISaveDataAssembleStrategy _dataAssembleStrategy;

        public SaveOnGameBoardStateChangedUseCase(IGameplayEventsDispatcher gameEvents, ISaver saver, ISaveDataAssembleStrategy dataAssembleStrategy)
        {
            _gameEvents = gameEvents;
            _saver = saver;
            _dataAssembleStrategy = dataAssembleStrategy;
        }

        public void Initialize() => 
            _gameEvents.OnBoardStateChanged += SaveGameState;

        public void Dispose() => 
            _gameEvents.OnBoardStateChanged -= SaveGameState;

        private void SaveGameState() => 
            _saver.Save(PersistenceConstants.SESSION_STATE_SAVE_KEY, _dataAssembleStrategy.AssembleSaveData());
    }
}