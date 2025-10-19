using Features.Playfield.App;
using Features.Score.App;
using Features.Score.Domain;
using Features.Score.Infrastructure;
using Libs.Bootstrap;
using Libs.Core.Patterns.Snapshot;
using UnityEngine;

namespace Features.Score.Composition
{
    public class ScoreInstaller : Installer
    {
        [SerializeField] private ScoreConfig _config;
        [SerializeField] private ScoreDisplayView _scoreDisplay;
        
        public override void Install(IInstallableContext context)
        {
            var model = new ScoreTracker(_config);

            var handleEventsUseCase = new MarshalLinesClearedEvents(model, context.Get<IPlayfieldEventsDispatcher>());
            context.RegisterRunnable(handleEventsUseCase);
            
            var scoreDisplayPresenter = new ScoreDisplayPresenter(model, _scoreDisplay);
            context.RegisterRunnable(scoreDisplayPresenter);
            
            context.RegisterContract<ISnapshotable<ScoreSnapshot>>(new MementoToSnapshotAdapter<ScoreMemento, ScoreSnapshot>(mementoProvider: model, mementoConsumer: model));
        }
    }
}