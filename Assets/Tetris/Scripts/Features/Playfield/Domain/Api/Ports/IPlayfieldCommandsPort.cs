namespace Features.Playfield.Domain.Api
{
    public interface IPlayfieldCommandsPort
    {
        void SetCommand(PlayfieldCommand command);
    }
}