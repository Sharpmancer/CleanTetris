using Features.Input.App;
using UnityEngine;

namespace Features.Input.Infrastructure
{
    public interface IInputCommandsToKeyCodesMapping
    {
        KeyCode GetKeyCode(InboundCommands command);
    }
}