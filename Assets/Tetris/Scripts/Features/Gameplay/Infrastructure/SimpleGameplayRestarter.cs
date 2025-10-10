using Features.Gameplay.App;
using UnityEngine.SceneManagement;

namespace Features.Gameplay.Infrastructure
{
    public class SimpleGameplayRestarter : IGameplayRestarter
    {
        public void Restart() => 
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}