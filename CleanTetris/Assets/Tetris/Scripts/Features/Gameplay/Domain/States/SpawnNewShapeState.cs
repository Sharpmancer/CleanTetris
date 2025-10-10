namespace Features.Gameplay.Domain.States
{
    internal class SpawnNewShapeState : GameplayState
    {
        internal override void Enter(GameplayMediator gameplay)
        {
            gameplay.CurrentShape = Shape.Random;
            gameplay.ShapePosition = new GridCoordinates(column: gameplay.Board.Columns / 2 - gameplay.CurrentShape.Width / 2, row: 0);

            var canFit = gameplay.Board.CanFit(gameplay.CurrentShape, gameplay.ShapePosition);
            gameplay.Board.PlaceShape(gameplay.CurrentShape, gameplay.ShapePosition, skipFitCheck: !canFit);
            gameplay.HandleBoardStateChanged();
            gameplay.HandleNewShapeSpawned();

            if (canFit)
                gameplay.ChangeState<IdleState>();
            else
                gameplay.ChangeState<GameOverState>();
        }
    }
}