using Features.MainMenu.App;
using Features.MainMenu.Infrastructure;
using Libs.Bootstrap;
using Libs.SceneManagement;
using UnityEngine;

namespace Features.MainMenu.Composition
{
    public class MainMenuInstaller : Installer
    {
        [SerializeField] private MainMenuDialogue _menuDialogue;
        
        public override void Install(IInstallableContext context) => 
            context.RegisterRunnable(new HandleMainMenuCommandsUseCase(_menuDialogue, context.Get<ISceneManager>()));
    }
}