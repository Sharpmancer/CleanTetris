using System;

namespace Features.Input.App
{
    public interface IOutboundInputCommandDispatcher
    {
        event Action<OutboundCommand> OnNewCommand;
    }
}