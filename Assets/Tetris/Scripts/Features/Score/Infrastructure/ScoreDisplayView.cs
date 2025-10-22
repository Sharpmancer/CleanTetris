using Features.Score.App;
using Features.Score.App.Internals;
using TMPro;
using UnityEngine;

namespace Features.Score.Infrastructure
{
    public class ScoreDisplayView : MonoBehaviour, IScoreDisplayView
    {
        [SerializeField] private TMP_Text _scoreText;
        
        public void UpdateScore(int value) => 
            _scoreText.text = value.ToString();
    }
}