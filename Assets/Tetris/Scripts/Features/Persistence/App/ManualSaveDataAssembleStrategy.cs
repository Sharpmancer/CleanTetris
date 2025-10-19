using System;
using Features.Playfield.App;
using Features.Score.App;
using Libs.Core.Patterns.Snapshot;

namespace Features.Persistence.App
{
    public class ManualSaveDataAssembleStrategy : ISaveDataAssembleStrategy
    {
        private readonly ISnapshotable<PlayfieldSnapshot> _gameplaySnapshotable;
        private readonly ISnapshotable<ScoreSnapshot> _scoreSnapshotable;
        
        public Type DataAssemblyType => typeof(SessionStateDataAssembly);

        public ManualSaveDataAssembleStrategy(ISnapshotable<PlayfieldSnapshot> gameplaySnapshotable, ISnapshotable<ScoreSnapshot> scoreSnapshotable) 
        {
            _gameplaySnapshotable = gameplaySnapshotable;
            _scoreSnapshotable = scoreSnapshotable;
        }

        public object AssembleSaveData() => 
            new SessionStateDataAssembly(
                _gameplaySnapshotable.GetSnapshot(), 
                _scoreSnapshotable.GetSnapshot());

        public void DisassembleSaveData(object saveData)
        {
            _gameplaySnapshotable.SetSnapshot(((SessionStateDataAssembly)saveData).PlayfieldSnapshot);
            _scoreSnapshotable.SetSnapshot(((SessionStateDataAssembly)saveData).ScoreSnapshot);
        }
    }
}