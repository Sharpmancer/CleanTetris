namespace Features.Input.App
{
    public interface IInboundCommandsPort
    {
        void Push(InboundCommands input);
    }
}