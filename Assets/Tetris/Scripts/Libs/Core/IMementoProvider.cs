namespace Libs.Core
{
    public interface IMementoProvider<out T> where T : IMemento
    {
        T GetMemento();
    }
}