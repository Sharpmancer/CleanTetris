namespace Features.Gameplay.Domain
{
    public interface IGameplayMementoConsumer
    {
        void SetMemento(Memento memento);
    }
}