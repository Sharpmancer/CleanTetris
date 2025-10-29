using Features.Input.App.Api;

namespace Features.Input.App.Internals
{
    internal interface IResolveInputStrategy
    {
        InboundCommands Resolve(InboundCommands activeFlag, InboundCommands lastMask, InboundCommands currentMask);
    }
}