using System;
using Features.Input.App;
using Features.Playfield.Domain.Api;
using Libs.Core.Lifecycle;

namespace Features.Playfield.App
{
    internal class MarshalPlayerInputUseCase : IInitializable, IDisposable
    {
        private readonly IOutboundInputCommandDispatcher _inputCommandDispatcher;
        private readonly IPlayfieldCommandsPort _playfieldCommandsPort;

        internal MarshalPlayerInputUseCase(IOutboundInputCommandDispatcher inputCommandDispatcher, IPlayfieldCommandsPort playfieldCommandsPort)
        {
            _inputCommandDispatcher = inputCommandDispatcher;
            _playfieldCommandsPort = playfieldCommandsPort;
        }

        public void Initialize() => 
            _inputCommandDispatcher.OnNewCommand += MarshalCommand;

        public void Dispose() => 
            _inputCommandDispatcher.OnNewCommand -= MarshalCommand;

        private void MarshalCommand(OutboundCommand obj) =>
            _playfieldCommandsPort.SetCommand(obj switch
            {
                OutboundCommand.None => PlayfieldCommand.None,
                OutboundCommand.MoveLeft => PlayfieldCommand.MoveLeft,
                OutboundCommand.MoveRight => PlayfieldCommand.MoveRight,
                OutboundCommand.MoveDown => PlayfieldCommand.MoveDown,
                OutboundCommand.Rotate => PlayfieldCommand.Rotate,
                _ => throw new ArgumentOutOfRangeException(nameof(obj), obj, null)
            });
    }
}