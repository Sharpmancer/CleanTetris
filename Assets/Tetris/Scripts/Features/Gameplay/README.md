
# Gameplay Feature

Implements the core Tetris loop (spawn → move/rotate → lock → clear → compact → next). Cleanly separated into **Domain**, **Application**, **Infrastructure**, and **Composition**.

## Structure

- **Domain**
    - `Board` — bitmask-based grid ops (fit checks, set/clear cells, compaction).
    - `Shape` — 4×4 mask pieces and utilities.
    - `GameplayMediator` — runtime state holder (board, active shape/pos, gravity timers); exposes ports:
        - `IGameplayEvents` (`OnRowsCleared`, `OnBoardStateChanged`, `OnNewShapeSpawned`, `OnGameOver`)
        - `IGameplayCommandsPort` (`SetCommand(...)`)
        - `IBoardStateProvider` (readonly snapshot for presenters)
    - `GameplayStateMachine` + `States/*` — ticks the active state only:
        - `StartGameState`, `IdleState`, `MoveShapeState`, `RotateShapeState`,
          `LockShapeState`, `TryClearRowsState`, `CompactBoardState`,
          `SpawnNewShapeState`, `GameOverState`.
    - Value objects: `GridCoordinates`, `GridDirection`, `GameplayCommand`.

- **Application**
    - `BoardPresenter` — subscribes to `IGameplayEvents` and pushes snapshots to `IGameplayBoardDisplay`.
    - `MarshalPlayerInputUseCase` — translates **Input** feature’s outbound commands to `GameplayCommand`.
    - `ResetInputStateOnNewShapeSpawnedUseCase` — clears buffered input upon spawn.
    - `HandleGameOverUseCase` — drives game-over UI and restart flow.
    - `GameplayEventsDispatcher` — **narrow cross-feature bridge** (exposes `IGameplayEventsDispatcher` with `OnRowsCleared` only).

- **Infrastructure**
    - `OneBitDisplayToIBoardDisplayAdapter` — adapter to the visual grid component.
    - `GameOverDialogue` — simple message view for game-over.
    - `SimpleGameplayRestarter` — scene/game reset implementation.

- **Composition**
    - `GameplayInstaller` — composition root: binds mediator as ports, registers presenters/use cases, exposes `IGameplayEventsDispatcher`.


[← Back](../../../../../README.md)