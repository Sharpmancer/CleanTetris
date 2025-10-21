using System;
using Features.Playfield.Domain.Api;
using Libs.Core.Lifecycle;

namespace Features.Playfield.Domain.Model
{
    internal class IdleState : PlayfieldState, ITickable
    {
        private Playfield _gameplay;

        internal override void Enter(Playfield gameplay)
        {
            _gameplay ??= gameplay;

            if (_gameplay.CurrentShape == null) 
                _gameplay.ChangeState<SpawnNewShapeState>();
        }

        public void Tick(float timeDelta)
        {
            _gameplay.TimeSinceLastTick += timeDelta;
            
            if (_gameplay.TimeSinceLastTick >= _gameplay.GravityTickInterval) 
            {
                _gameplay.CurrentCommand = PlayfieldCommand.MoveDown; 
                _gameplay.TimeSinceLastTick = 0;
            }
            
            switch (_gameplay.CurrentCommand)
            {
                case PlayfieldCommand.None: return;
                case PlayfieldCommand.Rotate: 
                    _gameplay.ChangeState<RotateShapeState>();
                    break;
                case PlayfieldCommand.MoveDown 
                    or PlayfieldCommand.MoveLeft 
                    or PlayfieldCommand.MoveRight:
                    _gameplay.ChangeState<MoveShapeState>();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(_gameplay.CurrentCommand), _gameplay.CurrentCommand, null);
            }
        }
    }
}