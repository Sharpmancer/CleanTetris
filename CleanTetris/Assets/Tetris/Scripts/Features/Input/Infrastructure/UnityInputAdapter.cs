using System;
using System.Linq;
using Features.Input.App;
using Libs.Core;
using UnityEngine;

namespace Features.Input.Infrastructure
{
    public sealed class UnityInputAdapter : MonoBehaviour, ITickable, IInitializable
    {
        private readonly ExceptionHandler _exceptionHandler = new();
        
        private IInboundCommandsPort _listener;
        private IInputCommandsToKeyCodesMapping _keyMapping;
        private (InboundCommands command, KeyCode key)[] _keys;
        private InboundCommands _commandsBuffer;

        public void Construct(IInboundCommandsPort listener, IInputCommandsToKeyCodesMapping keyMapping)
        {
            _keyMapping = keyMapping;
            _listener = listener;
        }

        public void Initialize() => 
            _keys = Enum.GetValues(typeof(InboundCommands))
                .Cast<InboundCommands>()
                .Where(n => n != InboundCommands.None)
                .Select(n => (n, _keyMapping.GetKeyCode(n)))
                .ToArray();

        public void Tick(float deltaTime)
        {
            _exceptionHandler.ThrowIfAnyAndReset();
            
            _listener.Push(_commandsBuffer);
            _commandsBuffer = InboundCommands.None;
        }

        private void Update()
        {
            _exceptionHandler.RunSafely(UpdateLogic);
            return;

            void UpdateLogic()
            {
                foreach (var (command, key) in _keys)
                {
                    if(UnityEngine.Input.GetKey(key))
                        _commandsBuffer |= command;
                }
            }
        }
    }
}