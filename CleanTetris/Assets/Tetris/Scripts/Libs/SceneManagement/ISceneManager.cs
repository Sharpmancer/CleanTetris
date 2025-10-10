namespace Libs.SceneManagement
{
    public interface ISceneManager
    {
        void ChangeScene<T>(T scene) where T : LoadSceneArgs;
        void ChangeScene<T>() where T : LoadSceneArgs, new();
    }
}