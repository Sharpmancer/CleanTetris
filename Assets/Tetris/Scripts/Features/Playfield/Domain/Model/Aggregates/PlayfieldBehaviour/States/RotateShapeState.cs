namespace Features.Playfield.Domain.States
{
    internal class RotateShapeState : PlayfieldState
    {
        private readonly Shape _tempShape = new();
        
        internal override void Enter(Playfield gameplay)
        {
            gameplay.Board.RemoveShape(gameplay.CurrentShape, gameplay.ShapePosition);
            gameplay.CurrentShape.WriteTo(_tempShape);
            _tempShape.RotateClockwise();
            var canFit = gameplay.Board.CanFit(_tempShape, gameplay.ShapePosition);
            if (canFit)
            {
                _tempShape.WriteTo(gameplay.CurrentShape);
                gameplay.CurrentCommand = PlayfieldCommand.None;
                gameplay.Board.PlaceShape(gameplay.CurrentShape, gameplay.ShapePosition);
                gameplay.HandleBoardStateChanged();
            }
            else
                // returning the shape back without changes
                gameplay.Board.PlaceShape(gameplay.CurrentShape, gameplay.ShapePosition);
            
            gameplay.ChangeState<IdleState>();
        }
    }
}