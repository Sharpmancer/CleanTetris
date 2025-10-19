namespace Features.Playfield.Domain.States
{
    internal class GameOverState : PlayfieldState
    {
        internal override void Enter(Playfield gameplay) => 
            gameplay.HandleOnGameOver();
    }
}