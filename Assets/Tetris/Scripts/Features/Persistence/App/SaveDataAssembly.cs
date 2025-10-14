using System;
using Features.Gameplay.App;
using Features.Score.App;

namespace Features.Persistence.App
{
    [Serializable]
    public struct SaveDataAssembly
    {
        public GameplaySnapshot GameplaySnapshot;
        public ScoreSnapshot ScoreSnapshot;

        public SaveDataAssembly(GameplaySnapshot gameplaySnapshot, ScoreSnapshot scoreSnapshot)
        {
            GameplaySnapshot = gameplaySnapshot;
            ScoreSnapshot = scoreSnapshot;
        }
    }
}