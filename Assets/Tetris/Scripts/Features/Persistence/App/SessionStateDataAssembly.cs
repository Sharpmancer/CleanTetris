using System;
using Features.Playfield.App;
using Features.Score.App;
using UnityEngine.Serialization;

namespace Features.Persistence.App
{
    [Serializable]
    public struct SessionStateDataAssembly
    {
        public PlayfieldSnapshot PlayfieldSnapshot;
        public ScoreSnapshot ScoreSnapshot;

        public SessionStateDataAssembly(PlayfieldSnapshot playfieldSnapshot, ScoreSnapshot scoreSnapshot)
        {
            PlayfieldSnapshot = playfieldSnapshot;
            ScoreSnapshot = scoreSnapshot;
        }
    }
}