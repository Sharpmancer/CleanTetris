namespace Features.Gameplay.Domain
{
    public interface IGameplayCommandsPort
    {
        void SetCommand(GameplayCommand command);
    }
}