namespace Features.Gameplay.Domain.States
{
    internal class GameOverState : GameplayState
    {
        internal override void Enter(GameplayMediator gameplay) => 
            gameplay.HandleOnGameOver();
    }
}