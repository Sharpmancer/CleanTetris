
# Gameplay Feature

Implements the core Tetris loop (spawn → move/rotate → lock → clear → compact → next). Cleanly separated into **Domain**, **Application**, **Infrastructure**, and **Composition**.

## Key entities

- **Domain**
    - `Board` — bitmask-based grid ops (fit checks, set/clear cells, compaction).
    - `Shape` — 4×4 mask pieces and utilities.
    - `GameplayMediator` — runtime state holder (board, active shape/pos, gravity timers); 
  exposes ports, emits events, operates leveling and difficulty calculation strategies.
    - `GameplayStateMachine` + `States/*`:
        - `StartGameState`, `IdleState`, `MoveShapeState`, `RotateShapeState`,
          `LockShapeState`, `TryClearRowsState`, `CompactBoardState`,
          `SpawnNewShapeState`, `GameOverState`.
    - Value objects: `GridCoordinates`, `GridDirection`, `GameplayCommand`.

- **Application**
    - `BoardPresenter` — subscribes to `IGameplayEventsDispatcher` from domain and pushes board state snapshots to `IGameplayBoardDisplay`.
    - Usecases that translate events between domain and outside world

- **Infrastructure**
    - `OneBitDisplayToIBoardDisplayAdapter` — adapter to the visual grid component.
    - `GameOverDialogue` — simple message view for game-over.
    - `SimpleGameplayRestarter` — scene/game reset implementation.

- **Composition**
    - `GameplayInstaller` — composition root: binds mediator as ports, registers presenters/use cases.


[← Back](../../../../../README.md)