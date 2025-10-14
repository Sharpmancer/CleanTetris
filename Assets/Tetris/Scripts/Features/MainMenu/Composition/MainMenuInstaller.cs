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
        
        public override void Install(IInstallableContext context)
        {
            var menuHandler = new HandleMainMenuUseCase(_menuDialogue, context.Get<ISceneManager>());
            
            context.RegisterContract<IMainMenuEventsDispatcher>(menuHandler);
            context.RegisterContract<IContinueGameIsAvailableHydratable>(menuHandler);
            context.RegisterRunnable(menuHandler);
        }
    }
}