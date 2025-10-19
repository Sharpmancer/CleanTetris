namespace Features.Playfield.Domain
{
    public interface IPlayfieldCommandsPort
    {
        void SetCommand(PlayfieldCommand command);
    }
}