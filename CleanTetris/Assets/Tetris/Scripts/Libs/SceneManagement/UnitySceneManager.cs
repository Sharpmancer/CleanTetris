using UnityEngine.SceneManagement;

namespace Libs.SceneManagement
{
    public class UnitySceneManager : ISceneManager
    {
        public void ChangeScene<T>() where T : LoadSceneArgs, new() => 
            ChangeScene(new T());

        public void ChangeScene<T>(T args) where T : LoadSceneArgs => 
            SceneManager.LoadScene(args.Name);
    }
}