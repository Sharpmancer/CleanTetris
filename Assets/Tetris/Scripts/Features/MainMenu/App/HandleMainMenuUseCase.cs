using System;
using Libs.Core;
using Libs.SceneManagement;
using UnityEngine;

namespace Features.MainMenu.App
{
    public class HandleMainMenuUseCase : IInitializable, IMainMenuEventsDispatcher, IContinueGameIsAvailableHydratable
    {
        private readonly ISceneManager _sceneManager;
        private readonly IMainMenuDialogue _mainMenuDialogue;

        public event Action OnNewGame;

        public HandleMainMenuUseCase(IMainMenuDialogue mainMenuDialogue, ISceneManager sceneManager)
        {
            _mainMenuDialogue = mainMenuDialogue;
            _sceneManager = sceneManager;
        }
        
        public void Initialize() => 
            _mainMenuDialogue.Show(
                onExit: Application.Quit, 
                onNewGame: () =>
                {
                    OnNewGame?.Invoke();
                    _sceneManager.ChangeScene<GameplayLoadSceneArgs>();
                },
                onContinue: _sceneManager.ChangeScene<GameplayLoadSceneArgs>);

        public void SetContinueGameIsAvailable(bool value) => 
            _mainMenuDialogue.SetContinueButtonInteractable(value);
    }
}