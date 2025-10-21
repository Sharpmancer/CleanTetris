using System;
using Libs.Core.Lifecycle;
using Libs.SceneManagement;

namespace Features.Playfield.App
{
    internal class HandleGameOverUseCase : IInitializable, IDisposable
    {
        private readonly Domain.Api.IPlayfieldEventsDispatcher _playfieldEvents;
        private readonly IGameOverDialogueView _dialogueView;
        private readonly ISceneManager _sceneManager;

        internal HandleGameOverUseCase(Domain.Api.IPlayfieldEventsDispatcher playfieldEvents, IGameOverDialogueView dialogueView, ISceneManager sceneManager)
        {
            _playfieldEvents = playfieldEvents;
            _dialogueView = dialogueView;
            _sceneManager = sceneManager;
        }

        public void Initialize() => 
            _playfieldEvents.OnGameOver += ShowDialogue;

        public void Dispose() => 
            _playfieldEvents.OnGameOver -= ShowDialogue;

        private void ShowDialogue() => 
            _dialogueView.Show(
                onRestartClicked: _sceneManager.ChangeScene<GameplayLoadSceneArgs>, 
                onMainMenuClicked: _sceneManager.ChangeScene<MainMenuLoadSceneArgs>);
    }
}