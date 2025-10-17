using System;
using Libs.Core.Lifecycle;

namespace Features.Gameplay.Domain.States
{
    internal class IdleState : GameplayState, ITickable
    {
        private GameplayMediator _gameplay;

        internal override void Enter(GameplayMediator gameplay)
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
                _gameplay.CurrentCommand = GameplayCommand.MoveDown; 
                _gameplay.TimeSinceLastTick = 0;
            }
            
            switch (_gameplay.CurrentCommand)
            {
                case GameplayCommand.None: return;
                case GameplayCommand.Rotate: 
                    _gameplay.ChangeState<RotateShapeState>();
                    break;
                case GameplayCommand.MoveDown 
                    or GameplayCommand.MoveLeft 
                    or GameplayCommand.MoveRight:
                    _gameplay.ChangeState<MoveShapeState>();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(_gameplay.CurrentCommand), _gameplay.CurrentCommand, null);
            }
        }
    }
}