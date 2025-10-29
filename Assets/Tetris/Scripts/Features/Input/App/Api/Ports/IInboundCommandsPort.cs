namespace Features.Input.App.Api
{
    public interface IInboundCommandsPort
    {
        void Push(InboundCommands input);
    }
}