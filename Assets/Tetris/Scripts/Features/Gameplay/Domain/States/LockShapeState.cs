namespace Features.Gameplay.Domain.States
{
    internal class LockShapeState : GameplayState
    {
        internal override void Enter(GameplayMediator gameplay)
        {
            gameplay.CurrentShape = null;
            gameplay.ShapePosition = default;
            gameplay.HandleBoardStateChanged();
            gameplay.ChangeState<TryClearRowsState>();
        }
    }
}