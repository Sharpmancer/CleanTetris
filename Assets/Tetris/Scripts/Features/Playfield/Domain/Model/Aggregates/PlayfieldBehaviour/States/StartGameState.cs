namespace Features.Playfield.Domain.Model
{
    internal class StartGameState : PlayfieldState
    {
        internal override void Enter(Playfield gameplay) => 
            gameplay.ChangeState<IdleState>();
    }
}