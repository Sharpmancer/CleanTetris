using Features.Gameplay.App;
using Features.Score.App;
using Features.Score.Domain;
using Features.Score.Infrastructure;
using Libs.Bootstrap;
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

            var handleEventsUseCase = new MarshalLinesClearedEvents(model, context.Get<IGameplayEventsDispatcher>());
            context.RegisterRunnable(handleEventsUseCase);
            
            var scoreDisplayPresenter = new ScoreDisplayPresenter(model, _scoreDisplay);
            context.RegisterRunnable(scoreDisplayPresenter);
        }
    }
}