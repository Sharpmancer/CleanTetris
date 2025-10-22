using Features.Score.Domain.Model;
using Libs.Core.Patterns.Memento;

namespace Features.Score.Domain.Api
{
    public interface IScorePersistencePort : IMementoProvider<ScoreMemento>, IMementoConsumer<ScoreMemento>
    {
        
    }
}