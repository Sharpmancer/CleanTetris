using System;
using Libs.Core.Patterns.Memento;

namespace Libs.Core.Patterns.Snapshot
{
    public sealed class MementoToSnapshotAdapter<TMemento, TSnapshot> : ISnapshotable<TSnapshot>, ISnapshotable
        where TMemento : IMemento
        where TSnapshot : ISnapshot<TMemento>, new()
    {
        private readonly IMementoProvider<TMemento> _mementoProvider;
        private readonly IMementoConsumer<TMemento> _mementoConsumer;

        public Type SnapshotType => typeof(TSnapshot);
        
        public MementoToSnapshotAdapter(IMementoProvider<TMemento> mementoProvider, IMementoConsumer<TMemento> mementoConsumer)
        {
            _mementoProvider = mementoProvider;
            _mementoConsumer = mementoConsumer;
        }

        public void SetSnapshot(object snapshot) => 
            SetSnapshot((TSnapshot)snapshot);
        
        object ISnapshotable.GetSnapshot() => 
            GetSnapshot();
        
        public TSnapshot GetSnapshot() => 
            (TSnapshot)new TSnapshot().Hydrate(_mementoProvider.GetMemento());

        public void SetSnapshot(TSnapshot snapshot) => 
            _mementoConsumer.SetMemento(snapshot.ToMemento());
    }
}