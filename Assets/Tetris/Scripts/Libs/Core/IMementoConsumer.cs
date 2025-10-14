namespace Libs.Core
{
    public interface IMementoConsumer<in T> where T : IMemento
    {
        void SetMemento(T Memento);
    }
}