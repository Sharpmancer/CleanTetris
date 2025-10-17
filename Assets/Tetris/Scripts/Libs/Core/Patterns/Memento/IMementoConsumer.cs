namespace Libs.Core.Patterns.Memento
{
    public interface IMementoConsumer<in T> where T : IMemento
    {
        void SetMemento(T Memento);
    }
}