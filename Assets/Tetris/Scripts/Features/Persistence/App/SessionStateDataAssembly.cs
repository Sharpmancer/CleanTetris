using System;
using Features.Gameplay.App;
using Features.Score.App;

namespace Features.Persistence.App
{
    [Serializable]
    public struct SessionStateDataAssembly
    {
        public GameplaySnapshot GameplaySnapshot;
        public ScoreSnapshot ScoreSnapshot;

        public SessionStateDataAssembly(GameplaySnapshot gameplaySnapshot, ScoreSnapshot scoreSnapshot)
        {
            GameplaySnapshot = gameplaySnapshot;
            ScoreSnapshot = scoreSnapshot;
        }
    }
}