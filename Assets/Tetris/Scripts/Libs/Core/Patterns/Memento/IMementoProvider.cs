namespace Libs.Core.Patterns.Memento
{
    public interface IMementoProvider<out T> where T : IMemento
    {
        T GetMemento();
    }
}