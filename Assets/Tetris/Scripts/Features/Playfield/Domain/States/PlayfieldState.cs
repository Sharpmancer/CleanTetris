namespace Features.Playfield.Domain.States
{
    internal abstract class PlayfieldState
    {
        internal abstract void Enter(Playfield gameplay);
    }
}