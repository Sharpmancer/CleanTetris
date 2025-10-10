using System;
using Features.Gameplay.Domain;
using Libs.Core;
using Libs.SceneManagement;

namespace Features.Gameplay.App
{
    public class HandleGameOverUseCase : IInitializable, IDisposable
    {
        private readonly IGameplayEvents _gameplayEvents;
        private readonly IGameOverDialogueView _dialogueView;
        private readonly ISceneManager _sceneManager;

        public HandleGameOverUseCase(IGameplayEvents gameplayEvents, IGameOverDialogueView dialogueView, ISceneManager sceneManager)
        {
            _gameplayEvents = gameplayEvents;
            _dialogueView = dialogueView;
            _sceneManager = sceneManager;
        }

        public void Initialize() => 
            _gameplayEvents.OnGameOver += ShowDialogue;

        public void Dispose() => 
            _gameplayEvents.OnGameOver -= ShowDialogue;

        private void ShowDialogue() => 
            _dialogueView.Show(
                onRestartClicked: _sceneManager.ChangeScene<GameplayLoadSceneArgs>, 
                onMainMenuClicked: _sceneManager.ChangeScene<MainMenuLoadSceneArgs>);
    }
}