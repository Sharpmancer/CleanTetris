using Features.Input.App.Api;
using UnityEngine;

namespace Features.Input.Infrastructure.Config
{
    public interface IInputCommandsToKeyCodesMapping
    {
        KeyCode GetKeyCode(InboundCommands command);
    }
}