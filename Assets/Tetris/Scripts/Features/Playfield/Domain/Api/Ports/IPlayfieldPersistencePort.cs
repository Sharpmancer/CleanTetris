using Libs.Core.Patterns.Memento;

namespace Features.Playfield.Domain
{
    public interface IPlayfieldPersistencePort : IMementoProvider<PlayfieldMemento>, IMementoConsumer<PlayfieldMemento>
    {
        
    }
}