using Features.Playfield.App;
using Features.Score.App.Internals;
using Features.Score.Domain.Model;
using Features.Score.Infrastructure;
using Libs.Bootstrap;
using Libs.Core.Patterns.Snapshot;
using UnityEngine;

namespace Features.Score.Composition
{
    public class ScoreInstaller : Installer
    {
        [SerializeField] private ScoreDisplayView _scoreDisplay;
        
        public override void Install(IInstallableContext context)
        {
            var model = new Domain.Model.Score(new NesPointsPerRowsClearedCalculationStrategy());

            var handleEventsUseCase = new MarshalLinesClearedEvents(model, context.Get<IPlayfieldEventsDispatcher>());
            context.RegisterRunnable(handleEventsUseCase);
            
            var scoreDisplayPresenter = new ScoreDisplayPresenter(scoreEvents: model, scoreProvider: model, scoreDisplay: _scoreDisplay);
            context.RegisterRunnable(scoreDisplayPresenter);
            
            context.RegisterContract<ISnapshotable<ScoreSnapshot>>(new MementoToSnapshotAdapter<ScoreMemento, ScoreSnapshot>(mementoProvider: model, mementoConsumer: model));
        }
    }
}