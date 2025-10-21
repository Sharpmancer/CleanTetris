# Playfield (Feature)

Implements the **core Tetris playfield loop**  
(spawn → move/rotate → lock → clear → compact → next).  
Separated into **Domain**, **Application**, **Infrastructure**, and **Composition** layers.


---

## Folder Layout

```
Domain
├─ Api
│  ├─ Messages/
│     ├─ ClearedRowsIndices.cs
│  │  └─ PlayfieldCommand.cs
│  └─ Ports/
│     ├─ IPlayfieldCommandsPort.cs
│     ├─ IPlayfieldEventsDispatcher.cs
│     ├─ IPlayfieldPersistencePort.cs
│     └─ IPlayfieldStateProvider.cs
└─ Model
   ├─ Aggregates/
   │  └─ PlayfieldBehaviour/
   │     └─ Playfield.cs
   ├─ Entities/
   │  ├─ Board.cs
   │  └─ Shape.cs
   ├─ Persistence/
   │  ├─ PlayfieldMemento.cs
   │  └─ PlayfieldMementoOperator.cs
   ├─ Strategies/
   │  ├─ IGravityCalculationStrategy.cs
   │  ├─ ILevelCalculationStrategy.cs
   │  ├─ IShapeChoiceStrategy.cs
   │  ├─ BagOf7ShapeChoiceStrategy.cs
   │  ├─ RandomShapeChoiceStrategy.cs
   │  ├─ ClassicNesLookupGravityCalculationStrategy.cs
   │  └─ OneLevelPerTenRowsClearedCalculationStrategy.cs
   └─ ValueObjects/
      ├─ GridCoordinates.cs
      └─ GridDirection.cs
```

## Public API

Other features and layers could depend only on these interfaces and messages, everything else is internal:

| Component                        | Purpose                                                                      |
|----------------------------------|------------------------------------------------------------------------------|
| **`IPlayfieldCommandsPort`**     | Issue gameplay commands (move, rotate, drop, start, pause).                  |
| **`IPlayfieldStateProvider`**    | Query read-only runtime state (board snapshot, active shape, level, etc.).   |
| **`IPlayfieldEventsDispatcher`** | Subscribe to domain events (rows cleared, game over, state changed).         |
| **`PlayfieldCommand`**           | Strongly-typed message representing player actions.                          |
| **`ClearedRowsIndices`**         | Compact structure providing count and indices of rows cleared in one action. |

---

## Persistence API

Other features and layers could depend only on these interfaces and messages, everything else is internal:

| Component                        | Purpose                                                                    |
|----------------------------------|----------------------------------------------------------------------------|
| **`IPlayfieldPersistencePort`**  | Save or restore via domain memento.                                        |
| **`PlayfieldMemento`**           | Internal state data.                                                       |

---

## Core Model

### Aggregates
- **`Playfield`** — main aggregate coordinating gameplay logic and enforcing invariants:
    - Shapes never overlap or exceed bounds.
    - Locks occur only when resting.
    - Clear → compact → emit events happens atomically.

### Entities
- **`Board`** — bitmask-based grid (fit checks, set/clear cells, row compaction).
- **`Shape`** — 4×4 mask representation with rotation and offset utilities.

### Value Objects
- `GridCoordinates` — column/row pair.
- `GridDirection` — same as coordinates with additional constraints to always represent a correct grid direction (no 1:1 or 0:2 cases).

### Strategies
- `IGravityCalculationStrategy`, `ILevelCalculationStrategy`, `IShapeChoiceStrategy` — replaceable rule sets.
    - Default implementations:
        - `ClassicNesLookupGravityCalculationStrategy`
        - `OneLevelPerTenRowsClearedCalculationStrategy`
    - Already configurable via composition layer implementations:
        - `BagOf7ShapeChoiceStrategy`
        - `RandomShapeChoiceStrategy`

### Persistence
- `PlayfieldMemento` — serialization-agnostic internal state of the aggregate (app-level snapshots handle the rest).
- `PlayfieldMementoOperator` — handles snapshot creation and restoration.

---

## Collaboration Flow

1. **Input → Commands**  
   Input feature translates raw inputs into `PlayfieldCommand` and calls `IPlayfieldCommandsPort`.

2. **Rendering / UI**  
   Presenter subscribes to `IPlayfieldEventsDispatcher` and pushes snapshots from `IPlayfieldStateProvider` into visual adapters via `IPlayfieldDisplay`.

3. **Meta-systems (e.g., Score)**  
   Listen to `OnRowsCleared`  events to update score.

---
[← Back](../../../../../README.md)