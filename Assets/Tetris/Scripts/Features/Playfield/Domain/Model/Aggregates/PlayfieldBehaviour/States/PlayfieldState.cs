namespace Features.Playfield.Domain.Model
{
    internal abstract class PlayfieldState
    {
        internal abstract void Enter(Playfield gameplay);
    }
}