namespace Features.Playfield.Domain.States
{
    internal class StartGameState : PlayfieldState
    {
        internal override void Enter(Playfield gameplay) => 
            gameplay.ChangeState<IdleState>();
    }
}