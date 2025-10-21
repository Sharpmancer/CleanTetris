using System;
using Features.Playfield.Domain.Api;

namespace Features.Playfield.Domain.Model
{
    internal class MoveShapeState : PlayfieldState
    {
        internal override void Enter(Playfield gameplay)
        {
            var direction = gameplay.CurrentCommand switch {
                // shape moves in 0->board.Count direction
                PlayfieldCommand.MoveDown => GridDirection.Up,
                PlayfieldCommand.MoveLeft => GridDirection.Left,
                PlayfieldCommand.MoveRight => GridDirection.Right,
                _ => throw new ArgumentOutOfRangeException()
            };
            
            gameplay.Board.RemoveShape(gameplay.CurrentShape, gameplay.ShapePosition);
            var newPosition = gameplay.ShapePosition + direction;
            var canFit = gameplay.Board.CanFit(gameplay.CurrentShape, newPosition);
            if (canFit)
            {
                gameplay.CurrentCommand = PlayfieldCommand.None;
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