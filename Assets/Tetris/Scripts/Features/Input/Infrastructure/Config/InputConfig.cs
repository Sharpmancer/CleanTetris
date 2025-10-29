using System;
using System.Collections.Generic;
using Features.Input.App.Api;
using UnityEngine;

namespace Features.Input.Infrastructure.Config
{
    [CreateAssetMenu(menuName = "Configs/InputConfig", fileName = "InputConfig", order = 0)]
    public class InputConfig : ScriptableObject, IInputCommandsToKeyCodesMapping, IRepeatInputStrategyConfig
    {
        [Serializable]
        private struct KeyCodeCommandPair
        {
            public KeyCode KeyCode;
            public InboundCommands Command;
        }

        [field: SerializeField] public float StartRepeatDelay { get; private set; } = .2f;
        [field: SerializeField] public float RepeatInterval { get; private set; } = .1f;
        [SerializeField] private KeyCodeCommandPair[] _keyCodeCommandPairs;

        public KeyCode GetKeyCode(InboundCommands command)
        {
            for (var i = 0; i < _keyCodeCommandPairs.Length; i++)
            {
                if(_keyCodeCommandPairs[i].Command == command)
                    return _keyCodeCommandPairs[i].KeyCode;
            }
            throw new KeyNotFoundException("Key: " + command + " not found");
        }
    }
}