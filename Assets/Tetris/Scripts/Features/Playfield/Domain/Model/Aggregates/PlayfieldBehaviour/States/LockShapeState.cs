namespace Features.Playfield.Domain.Model
{
    internal class LockShapeState : PlayfieldState
    {
        internal override void Enter(Playfield gameplay)
        {
            gameplay.CurrentShape = null;
            gameplay.ShapePosition = default;
            gameplay.HandleBoardStateChanged();
            gameplay.ChangeState<TryClearRowsState>();
        }
    }
}