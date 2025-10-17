using System;
using Features.Gameplay.Domain.States;
using Libs.Bitmasks;
using Libs.Core.Lifecycle;
using Libs.Core.Primitives;

namespace Features.Gameplay.Domain
{
    public class GameplayMediator : IInitializable, ITickable, IGameplayEventsDispatcher, IBoardStateProvider, IGameplayCommandsPort, IDisposable
    {
        private readonly GameplayStateMachine _stateMachine;
        internal Board Board { get; }
        internal Shape CurrentShape { get; set; }
        internal GridCoordinates ShapePosition { get; set; }
        internal GameplayCommand CurrentCommand { get; set; }
        internal float GravityTickInterval { get; } = .75f;
        internal float TimeSinceLastTick { get; set; }

        public IReadOnlyBitMask2D BoardState => Board.Mask;
        public event Action<UpToFourBytes> OnRowsCleared;
        public event Action OnBoardStateChanged;
        public event Action OnNewShapeSpawned;
        public event Action OnGameOver;

        public GameplayMediator(int boardWidth, int boardHeight)
        {
            Board = new Board(boardWidth, boardHeight);
            _stateMachine = new GameplayStateMachine();
        }

        public void Initialize() =>
            ChangeState<StartGameState>();

        public void Tick(float timeDelta) => 
            _stateMachine.Tick(timeDelta);

        public void SetCommand(GameplayCommand command) => 
            CurrentCommand = command;

        internal void ChangeState<T>() where T : GameplayState => 
            _stateMachine.ChangeState<T>(this);

        internal void HandleOnGameOver() => 
            OnGameOver?.Invoke();

        internal void HandleBoardStateChanged() => 
            OnBoardStateChanged?.Invoke();

        internal void HandleRowsCleared(UpToFourBytes rows) =>
            OnRowsCleared?.Invoke(rows);

        internal void HandleNewShapeSpawned()
        {
            TimeSinceLastTick = 0;
            CurrentCommand = GameplayCommand.None;
            OnNewShapeSpawned?.Invoke();
        }

        public void Dispose() => 
            _stateMachine.Dispose();
    }
}