namespace Features.Gameplay.Domain.States
{
    internal abstract class GameplayState
    {
        internal abstract void Enter(GameplayMediator gameplay);
    }
}