using System;
using Features.Playfield.App;
using UnityEngine;
using UnityEngine.UI;

namespace Features.Playfield.Infrastructure
{
    public class GameOverDialogue : MonoBehaviour, IGameOverDialogueView
    {
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _mainMenuButton;
        private Action _onRestartClicked;
        private Action _onMainMenuClicked;

        private void Awake()
        {
            _restartButton.onClick.AddListener(HandleRestartClicked);
            _mainMenuButton.onClick.AddListener(HandleMainMenuClicked);
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            _restartButton.onClick.RemoveListener(HandleRestartClicked);
            _mainMenuButton.onClick.RemoveListener(HandleMainMenuClicked);
        }

        public void Show(Action onRestartClicked, Action onMainMenuClicked)
        {
            gameObject.SetActive(true);
            _onRestartClicked = onRestartClicked;
            _onMainMenuClicked = onMainMenuClicked;
        }

        private void HandleRestartClicked()
        {
            _onRestartClicked?.Invoke();
            gameObject.SetActive(false);
        }

        private void HandleMainMenuClicked()
        {
            _onMainMenuClicked?.Invoke();
            gameObject.SetActive(false);
        }
    }
}