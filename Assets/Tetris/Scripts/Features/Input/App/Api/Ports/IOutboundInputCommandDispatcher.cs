using System;

namespace Features.Input.App.Api
{
    public interface IOutboundInputCommandDispatcher
    {
        event Action<OutboundCommand> OnNewCommand;
    }
}