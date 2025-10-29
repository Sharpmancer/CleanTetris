namespace Features.Input.App
{
    internal interface IResolveInputStrategy
    {
        InboundCommands Resolve(InboundCommands activeFlag, InboundCommands lastMask, InboundCommands currentMask);
    }
}