using System;
using Features.Playfield.Domain.States;
using Libs.Bitmasks;
using Libs.Core.Lifecycle;
using Libs.Core.Primitives;

namespace Features.Playfield.Domain
{
    public class Playfield : IPlayfieldEventsDispatcher, IPlayfieldStateProvider, IPlayfieldCommandsPort, IInitializable, ITickable, IDisposable
    {
        private readonly PlayfieldStateMachine _stateMachine;
        private readonly IGravityCalculationStrategy _gravityCalculationStrategy;
        private readonly ILevelCalculationStrategy _levelCalculationStrategy;

        internal Board Board { get; }
        internal Shape CurrentShape { get; set; }
        internal GridCoordinates ShapePosition { get; set; }
        internal PlayfieldCommand CurrentCommand { get; set; }
        internal int TotalRowsCleared { get; set; }
        internal float TimeSinceLastTick { get; set; }
        internal float GravityTickInterval { get; private set; }

        public IReadOnlyBitMask2D BoardState => Board.Mask;
        public event Action<UpToFourBytes> OnRowsCleared;
        public event Action OnBoardStateChanged;
        public event Action OnNewShapeSpawned;
        public event Action OnGameOver;

        public Playfield(int boardWidth, int boardHeight, IGravityCalculationStrategy gravityCalculationStrategy, ILevelCalculationStrategy levelCalculationStrategy)
        {
            _gravityCalculationStrategy = gravityCalculationStrategy;
            _levelCalculationStrategy = levelCalculationStrategy;
            Board = new Board(boardWidth, boardHeight);
            _stateMachine = new PlayfieldStateMachine();
        }

        public void Initialize()
        {
            RecalculateGravity();
            ChangeState<StartGameState>();
        }

        public void Tick(float timeDelta) => 
            _stateMachine.Tick(timeDelta);

        public void SetCommand(PlayfieldCommand command) => 
            CurrentCommand = command;

        internal void ChangeState<T>() where T : PlayfieldState => 
            _stateMachine.PlayfieldState<T>(this);

        internal void HandleOnGameOver() => 
            OnGameOver?.Invoke();

        internal void HandleBoardStateChanged() => 
            OnBoardStateChanged?.Invoke();

        internal void HandleRowsCleared(UpToFourBytes rows)
        {
            TotalRowsCleared += rows.Count;
            RecalculateGravity();
            OnRowsCleared?.Invoke(rows);
        }

        internal void RecalculateGravity()
        {
            var level = _levelCalculationStrategy.GetLevel(TotalRowsCleared);
            GravityTickInterval = _gravityCalculationStrategy.GetFallRowDuration(level);
        }

        internal void HandleNewShapeSpawned()
        {
            TimeSinceLastTick = 0;
            CurrentCommand = PlayfieldCommand.None;
            OnNewShapeSpawned?.Invoke();
        }

        public void Dispose() => 
            _stateMachine.Dispose();
    }
}