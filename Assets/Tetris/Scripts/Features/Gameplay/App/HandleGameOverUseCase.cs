using System;
using Libs.Core.Lifecycle;
using Libs.SceneManagement;

namespace Features.Gameplay.App
{
    public class HandleGameOverUseCase : IInitializable, IDisposable
    {
        private readonly Domain.IGameplayEventsDispatcher _gameplayEvents;
        private readonly IGameOverDialogueView _dialogueView;
        private readonly ISceneManager _sceneManager;

        public HandleGameOverUseCase(Domain.IGameplayEventsDispatcher gameplayEvents, IGameOverDialogueView dialogueView, ISceneManager sceneManager)
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