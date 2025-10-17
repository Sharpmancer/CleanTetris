using System;
using Features.Gameplay.Domain;
using Features.Input.App;
using Libs.Core.Lifecycle;

namespace Features.Gameplay.App
{
    public class MarshalPlayerInputUseCase : IInitializable, IDisposable
    {
        private readonly IOutboundInputCommandDispatcher _inputCommandDispatcher;
        private readonly IGameplayCommandsPort _gameplayCommandsPort;

        public MarshalPlayerInputUseCase(IOutboundInputCommandDispatcher inputCommandDispatcher, IGameplayCommandsPort gameplayCommandsPort)
        {
            _inputCommandDispatcher = inputCommandDispatcher;
            _gameplayCommandsPort = gameplayCommandsPort;
        }

        public void Initialize() => 
            _inputCommandDispatcher.OnNewCommand += MarshalCommand;

        public void Dispose() => 
            _inputCommandDispatcher.OnNewCommand -= MarshalCommand;

        private void MarshalCommand(OutboundCommand obj) =>
            _gameplayCommandsPort.SetCommand(obj switch
            {
                OutboundCommand.None => GameplayCommand.None,
                OutboundCommand.MoveLeft => GameplayCommand.MoveLeft,
                OutboundCommand.MoveRight => GameplayCommand.MoveRight,
                OutboundCommand.MoveDown => GameplayCommand.MoveDown,
                OutboundCommand.Rotate => GameplayCommand.Rotate,
                _ => throw new ArgumentOutOfRangeException(nameof(obj), obj, null)
            });
    }
}