using Libs.Core.Patterns.Memento;

namespace Libs.Core.Patterns.Snapshot
{
    /// <summary>
    /// For generic constraints and usability inside IDE
    /// </summary>
    public interface ISnapshot
    {
        
    }

    public interface ISnapshot<TMemento> : ISnapshot where TMemento : IMemento
    {
        TMemento ToMemento();
        ISnapshot<TMemento> Hydrate(TMemento memento);
    }
}