namespace Features.Gameplay.Domain.States
{
    internal class StartGameState : GameplayState
    {
        internal override void Enter(GameplayMediator gameplay) => 
            gameplay.ChangeState<SpawnNewShapeState>();
    }
}