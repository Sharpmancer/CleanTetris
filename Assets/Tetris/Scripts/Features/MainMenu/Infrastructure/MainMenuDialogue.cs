using System;
using Features.MainMenu.App;
using UnityEngine;
using UnityEngine.UI;

namespace Features.MainMenu.Infrastructure
{
    public class MainMenuDialogue : MonoBehaviour, IMainMenuDialogue
    {
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _newGameButton;
        [SerializeField] private Button _exitButton;
        
        private Action _onContinue;
        private Action _onNewGame;
        private Action _onExit;

        private void Awake()
        {
            _exitButton.onClick.AddListener(HandleExitClicked);
            _newGameButton.onClick.AddListener(HandleNewGameClicked);
            _continueButton.onClick.AddListener(HandleContinueClicked);
        }

        private void OnDestroy()
        {
            _exitButton.onClick.RemoveListener(HandleExitClicked);
            _newGameButton.onClick.RemoveListener(HandleNewGameClicked);
            _continueButton.onClick.AddListener(HandleContinueClicked);
        }

        public void Show(Action onExit, Action onNewGame, Action onContinue)
        {
            _onExit = onExit;
            _onNewGame = onNewGame;
            _onContinue = onContinue;
            gameObject.SetActive(true);
        }

        public void SetContinueButtonInteractable(bool value) => 
            _continueButton.interactable = value;

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

        private void HandleContinueClicked()
        {
            _onContinue?.Invoke();
            gameObject.SetActive(false);
        }
    }
}