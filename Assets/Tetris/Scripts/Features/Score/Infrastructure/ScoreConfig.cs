using Features.Score.Domain;
using UnityEngine;

namespace Features.Score.Infrastructure
{
    
    [CreateAssetMenu(menuName = "Configs/ScoreConfig", fileName = "ScoreConfig", order = 0)]
    public class ScoreConfig : ScriptableObject, IScoreConfig
    {
        [field: SerializeField] public int PointsForOneRow { get; private set; } = 40;
        [field: SerializeField] public int PointsForTwoRows { get; private set; } = 100;
        [field: SerializeField] public int PointsForThreeRows { get; private set; } = 300;
        [field: SerializeField] public int PointsForFourRows { get; private set; } = 1300;
    }
}