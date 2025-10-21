using System;
using Features.Input.App;
using Features.Playfield.App;
using Features.Playfield.Domain.Api;
using Features.Playfield.Domain.Model;
using Features.Playfield.Infrastructure;
using Libs.Bootstrap;
using Libs.Core.Patterns.Snapshot;
using Libs.Core.Rng;
using Libs.OneBitDisplay;
using Libs.SceneManagement;
using UnityEngine;
using IPlayfieldEventsDispatcher = Features.Playfield.Domain.Api.IPlayfieldEventsDispatcher;

namespace Features.Playfield.Composition
{
    public class PlayfieldInstaller : Installer
    {
        private enum ShapeSpawnRandomType
        {
            PureRandom,
            BagOf7,
        }
        
        [SerializeField] private ShapeSpawnRandomType _shapeSpawnRandomType;
        [SerializeField] private Vector2Int _boardSize = new(10, 20);
        [SerializeField] private OneBitDisplay _nativeDisplay;
        [SerializeField] private GameOverDialogue _gameOverDialogue;
        
        public override void Install(IInstallableContext context)
        {
            var model = new Domain.Model.Playfield(_boardSize.x, _boardSize.y, 
                new ClassicNesLookupGravityCalculationStrategy(), 
                new OneLevelPerTenRowsClearedCalculationStrategy(),
                _shapeSpawnRandomType switch {
                        ShapeSpawnRandomType.PureRandom => new RandomShapeChoiceStrategy(new SystemRandomBasedRng()),
                        ShapeSpawnRandomType.BagOf7 => new BagOf7ShapeChoiceStrategy(),
                        _ => throw new ArgumentOutOfRangeException()
                    }
                );
            context.RegisterContract<IPlayfieldEventsDispatcher>(model);
            context.RegisterContract<IPlayfieldStateProvider>(model);
            context.RegisterContract<IPlayfieldCommandsPort>(model);
            context.RegisterRunnable(model);

            var snapshotAdapter = new MementoToSnapshotAdapter<PlayfieldMemento, PlayfieldSnapshot>(mementoProvider: model, mementoConsumer: model);
            context.RegisterContract<ISnapshotable<PlayfieldSnapshot>>(snapshotAdapter);

            var displayAdapter = new OneBitDisplayToIBoardDisplayAdapter(_nativeDisplay);
            var presenter = new BoardPresenter(context.Get<IPlayfieldEventsDispatcher>(), context.Get<IPlayfieldStateProvider>(), displayAdapter);
            context.RegisterRunnable(presenter);

            var marshalInputUseCase = new MarshalPlayerInputUseCase(context.Get<IOutboundInputCommandDispatcher>(), context.Get<IPlayfieldCommandsPort>());
            context.RegisterRunnable(marshalInputUseCase);

            var resetInputUseCase = new ResetInputStateOnNewShapeSpawnedUseCase(context.Get<IPlayfieldEventsDispatcher>(), context.Get<IInputStateResetter>());
            context.RegisterRunnable(resetInputUseCase);

            var handleGameOverUseCase = new HandleGameOverUseCase(context.Get<IPlayfieldEventsDispatcher>(), _gameOverDialogue, context.Get<ISceneManager>());
            context.RegisterRunnable(handleGameOverUseCase);
            
            var eventsDispatcher = new PlayfieldEventsDispatcher(model);
            context.RegisterContract<App.IPlayfieldEventsDispatcher>(eventsDispatcher);
            context.RegisterRunnable(eventsDispatcher);
        }
    }
}