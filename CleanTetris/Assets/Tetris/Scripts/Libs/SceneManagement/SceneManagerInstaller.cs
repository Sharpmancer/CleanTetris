using Libs.Bootstrap;

namespace Libs.SceneManagement
{
    public class SceneManagerInstaller : Installer
    {
        public override void Install(IInstallableContext context) => 
            context.RegisterContract<ISceneManager>(new UnitySceneManager());
    }
}