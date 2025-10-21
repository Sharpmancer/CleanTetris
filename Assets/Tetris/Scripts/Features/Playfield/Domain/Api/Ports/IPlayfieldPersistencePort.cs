using Features.Playfield.Domain.Model;
using Libs.Core.Patterns.Memento;

namespace Features.Playfield.Domain.Api
{
    public interface IPlayfieldPersistencePort : IMementoProvider<PlayfieldMemento>, IMementoConsumer<PlayfieldMemento>
    {
        
    }
}