using Features.Gameplay.App;
using Features.Gameplay.Domain;
using Features.Gameplay.Infrastructure;
using Features.Input.App;
using Libs.Bootstrap;
using Libs.Core.Patterns.Memento;
using Libs.Core.Patterns.Snapshot;
using Libs.OneBitDisplay;
using Libs.SceneManagement;
using UnityEngine;
using IGameplayEventsDispatcher = Features.Gameplay.Domain.IGameplayEventsDispatcher;

namespace Features.Gameplay.Composition
{
    public class GameplayInstaller : Installer
    {
        [SerializeField] private Vector2Int _boardSize = new(10, 20);
        [SerializeField] private OneBitDisplay _nativeDisplay;
        [SerializeField] private GameOverDialogue _gameOverDialogue;
        
        public override void Install(IInstallableContext context)
        {
            var model = new GameplayMediator(_boardSize.x, _boardSize.y);
            context.RegisterContract<IGameplayEventsDispatcher>(model);
            context.RegisterContract<IBoardStateProvider>(model);
            context.RegisterContract<IGameplayCommandsPort>(model);
            context.RegisterRunnable(model);

            var mementoOperator = new GameplayMementoOperator(model);
            context.RegisterContract<IMementoProvider<GameplayMemento>>(mementoOperator);
            context.RegisterContract<IMementoConsumer<GameplayMemento>>(mementoOperator);
            
            var snapshotOperator = new GameplaySnapshotOperator(mementoProvider: mementoOperator, mementoConsumer: mementoOperator);
            context.RegisterContract<ISnapshotable<GameplaySnapshot>>(snapshotOperator);

            var displayAdapter = new OneBitDisplayToIBoardDisplayAdapter(_nativeDisplay);
            var presenter = new BoardPresenter(context.Get<IGameplayEventsDispatcher>(), context.Get<IBoardStateProvider>(), displayAdapter);
            context.RegisterRunnable(presenter);

            var marshalInputUseCase = new MarshalPlayerInputUseCase(context.Get<IOutboundInputCommandDispatcher>(), context.Get<IGameplayCommandsPort>());
            context.RegisterRunnable(marshalInputUseCase);

            var resetInputUseCase = new ResetInputStateOnNewShapeSpawnedUseCase(context.Get<IGameplayEventsDispatcher>(), context.Get<IInputStateResetter>());
            context.RegisterRunnable(resetInputUseCase);

            var handleGameOverUseCase = new HandleGameOverUseCase(context.Get<IGameplayEventsDispatcher>(), _gameOverDialogue, context.Get<ISceneManager>());
            context.RegisterRunnable(handleGameOverUseCase);
            
            var eventsDispatcher = new GameplayEventsDispatcher(model);
            context.RegisterContract<App.IGameplayEventsDispatcher>(eventsDispatcher);
            context.RegisterRunnable(eventsDispatcher);
        }
    }
}