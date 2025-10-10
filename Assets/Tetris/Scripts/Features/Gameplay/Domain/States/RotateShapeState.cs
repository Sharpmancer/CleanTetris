namespace Features.Gameplay.Domain.States
{
    internal class RotateShapeState : GameplayState
    {
        private readonly Shape _tempShape = new();
        
        internal override void Enter(GameplayMediator gameplay)
        {
            gameplay.Board.RemoveShape(gameplay.CurrentShape, gameplay.ShapePosition);
            gameplay.CurrentShape.WriteTo(_tempShape);
            _tempShape.RotateClockwise();
            var canFit = gameplay.Board.CanFit(_tempShape, gameplay.ShapePosition);
            if (canFit)
            {
                _tempShape.WriteTo(gameplay.CurrentShape);
                gameplay.CurrentCommand = GameplayCommand.None;
                gameplay.Board.PlaceShape(gameplay.CurrentShape, gameplay.ShapePosition);
                gameplay.HandleBoardStateChanged();
            }
            
            gameplay.ChangeState<IdleState>();
        }
    }
}