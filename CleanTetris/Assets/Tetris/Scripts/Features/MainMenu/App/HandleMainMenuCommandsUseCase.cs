using Libs.Core;
using Libs.SceneManagement;
using UnityEngine;

namespace Features.MainMenu.App
{
    public class HandleMainMenuCommandsUseCase : IInitializable
    {
        private readonly ISceneManager _sceneManager;
        private readonly IMainMenuDialogue _mainMenuDialogue;

        public HandleMainMenuCommandsUseCase(IMainMenuDialogue mainMenuDialogue, ISceneManager sceneManager)
        {
            _mainMenuDialogue = mainMenuDialogue;
            _sceneManager = sceneManager;
        }

        public void Initialize() => 
            _mainMenuDialogue.Show(onExit: Application.Quit, onNewGame: _sceneManager.ChangeScene<GameplayLoadSceneArgs>);
    }
}