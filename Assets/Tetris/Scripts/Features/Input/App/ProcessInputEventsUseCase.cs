using System;
using Libs.Core.Lifecycle;

namespace Features.Input.App
{
    public class ProcessInputEventsUseCase : IInboundCommandsPort, IOutboundInputCommandDispatcher, ITickable, IInputStateResetter
    {
        private readonly IRepeatHeldInputStrategy _repeatInputStrategy;
        private readonly IResolveInputStrategy _resolveStrategy;
        
        private InboundCommands _activeCommand;
        private InboundCommands _lastCommandsMask;

        public event Action<OutboundCommand> OnNewCommand;

        public ProcessInputEventsUseCase(IRepeatHeldInputStrategy repeatInputStrategy, IResolveInputStrategy resolveStrategy)
        {
            _repeatInputStrategy = repeatInputStrategy;
            _resolveStrategy = resolveStrategy;
        }

        public void Tick(float deltaTime)
        {
            if(_activeCommand == InboundCommands.None)
                return;
            
            _repeatInputStrategy.ProcessTimePassed(deltaTime, out var repeat);
            if(repeat)
                RaiseNewOutboundCommand();
        }

        public void Push(InboundCommands commandsMask)
        {
            var newActiveCommand = _resolveStrategy.Resolve(activeFlag: _activeCommand, lastMask: _lastCommandsMask, currentMask: commandsMask);
            
            if (newActiveCommand != _activeCommand)
            {
                _activeCommand = newActiveCommand;
                _repeatInputStrategy.Reset();
                if(_activeCommand != InboundCommands.None)
                    RaiseNewOutboundCommand();
            }

            _lastCommandsMask = commandsMask;
        }

        public void Reset()
        {
            _repeatInputStrategy.Reset();
            _activeCommand = InboundCommands.None;
            _lastCommandsMask = InboundCommands.None;
        }

        private void RaiseNewOutboundCommand() =>
            OnNewCommand?.Invoke(_activeCommand switch
            {
                InboundCommands.Left => OutboundCommand.MoveLeft,
                InboundCommands.Right => OutboundCommand.MoveRight,
                InboundCommands.Rotate => OutboundCommand.Rotate,
                InboundCommands.Down => OutboundCommand.MoveDown,
                _ => throw new ArgumentOutOfRangeException(nameof(_activeCommand), _activeCommand, null)
            });
    }
}