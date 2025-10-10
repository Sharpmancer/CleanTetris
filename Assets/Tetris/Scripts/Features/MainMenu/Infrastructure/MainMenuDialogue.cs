using System;
using Features.MainMenu.App;
using UnityEngine;
using UnityEngine.UI;

namespace Features.MainMenu.Infrastructure
{
    public class MainMenuDialogue : MonoBehaviour, IMainMenuDialogue
    {
        [SerializeField] private Button _exitButton;
        [SerializeField] private Button _newGameButton;
        
        private Action _onExit;
        private Action _onNewGame;

        private void Awake()
        {
            _exitButton.onClick.AddListener(HandleExitClicked);
            _newGameButton.onClick.AddListener(HandleNewGameClicked);
        }

        private void OnDestroy()
        {
            _exitButton.onClick.RemoveListener(HandleExitClicked);
            _newGameButton.onClick.RemoveListener(HandleNewGameClicked);
        }

        public void Show(Action onExit, Action onNewGame)
        {
            _onExit = onExit;
            _onNewGame = onNewGame;
            gameObject.SetActive(true);
        }

        private void HandleNewGameClicked()
        {
            _onNewGame?.Invoke();
            gameObject.SetActive(false);
        }

        private void HandleExitClicked()
        {
            _onExit?.Invoke();
            gameObject.SetActive(false);
        }
    }
}