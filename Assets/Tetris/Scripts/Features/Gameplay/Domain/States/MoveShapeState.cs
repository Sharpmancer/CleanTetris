using System;

namespace Features.Gameplay.Domain.States
{
    internal class MoveShapeState : GameplayState
    {
        internal override void Enter(GameplayMediator gameplay)
        {
            var direction = gameplay.CurrentCommand switch {
                // shape moves in 0->board.Count direction
                GameplayCommand.MoveDown => GridDirection.Up,
                GameplayCommand.MoveLeft => GridDirection.Left,
                GameplayCommand.MoveRight => GridDirection.Right,
                _ => throw new ArgumentOutOfRangeException()
            };
            
            gameplay.Board.RemoveShape(gameplay.CurrentShape, gameplay.ShapePosition);
            var newPosition = gameplay.ShapePosition + direction;
            var canFit = gameplay.Board.CanFit(gameplay.CurrentShape, newPosition);
            if (canFit)
            {
                gameplay.CurrentCommand = GameplayCommand.None;
                gameplay.ShapePosition = newPosition;
            }
            gameplay.Board.PlaceShape(gameplay.CurrentShape, gameplay.ShapePosition);
            if(canFit)
                gameplay.HandleBoardStateChanged();
            
            // if hits bottom (why up - see comments above) - proceed with try to clear -> compact -> spawn new etc
            if(!canFit && direction == GridDirection.Up)
                gameplay.ChangeState<LockShapeState>();
            // otherwise continue dropping 
            else
                gameplay.ChangeState<IdleState>();
        }
    }
}