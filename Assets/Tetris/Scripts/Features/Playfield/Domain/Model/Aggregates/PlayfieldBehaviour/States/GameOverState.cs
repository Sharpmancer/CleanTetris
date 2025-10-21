namespace Features.Playfield.Domain.Model
{
    internal class GameOverState : PlayfieldState
    {
        internal override void Enter(Playfield gameplay) => 
            gameplay.HandleOnGameOver();
    }
}