namespace Features.Input.App
{
    public interface IResolveInputStrategy
    {
        InboundCommands Resolve(InboundCommands activeFlag, InboundCommands lastMask, InboundCommands currentMask);
    }
}