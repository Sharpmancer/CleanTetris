# 🧮 Score (Feature)

Implements an **independent scoring system** that listens to **Gameplay’s Rows-Cleared** events and updates the current score accordingly.  
Separated into **Domain**, **Application**, **Infrastructure**, and **Composition** layers.

---

## Folder Layout

```
Domain
├─ Api
│  └─ Ports/
│     ├─ ILinesClearedHandler.cs
│     ├─ IScoreEventsDispatcher.cs
│     ├─ IScorePersistencePort.cs
│     └─ IScoreProvider.cs
├─ Model
│  ├─ Aggregates/
│  │  └─ Score.cs
│  ├─ Persistence/
│  │  └─ ScoreMemento.cs
│  └─ Strategies/
│     ├─ IPointsPerRowsClearedCalculationStrategy.cs
│     └─ NesPointsPerRowsClearedCalculationStrategy.cs
App
├─ Internals/
│  ├─ Persistence/
│  │  └─ ScoreSnapshot.cs
│  ├─ Presenters/
│  │  └─ ScoreDisplayPresenter.cs
│  ├─ UseCases/
│  │  └─ MarshalLinesClearedEvents.cs
│  └─ ViewAbstractions/
│     └─ IScoreDisplayView.cs
Infrastructure
└─ ScoreDisplayView.cs
Composition
└─ ScoreInstaller.cs
```

---

## Public API

Other features and layers should depend only on these interfaces and messages — everything else is internal:

| Component | Purpose |
|------------|----------|
| **`ILinesClearedHandler`** | Contract for reacting to playfield’s row-cleared events. |
| **`IScoreProvider`** | Read-only access to current score. |
| **`IScoreEventsDispatcher`** | Notifies listeners (UI, other features) about score changes. |
| **`IScorePersistencePort`** | Exposes score snapshot load/save operations. |

---

## Core Model

### Aggregates
- **`Score`** — main aggregate maintaining the total score and raising domain events:
  - Receives line-clear notifications.
  - Calculates score according to strategy.
  - Dispatches events via `IScoreEventsDispatcher`.

### Strategies
- **`IPointsPerRowsClearedCalculationStrategy`** — defines how many points are granted for 1–4 cleared lines.
- **`NesPointsPerRowsClearedCalculationStrategy`** — NES-style lookup table.

### Persistence
- **`ScoreMemento`** — internal, serialization-agnostic state for persistence.
- **`IScorePersistencePort`** — interface to persist/restore score state externally (used by save system).

---

## Application Layer

### Use Cases
- **`MarshalLinesClearedEvents`** — subscribes to `IGameplayEventsDispatcher.OnRowsCleared` and forwards those to the domain aggregate.

### Presenters
- **`ScoreDisplayPresenter`** — observes score change events and updates the UI via `IScoreDisplayView`.

### View Abstractions
- **`IScoreDisplayView`** — contract for UI elements that visualize the current score.

### Persistence
- **`ScoreSnapshot`** — application-level data container used when saving/loading between scenes or sessions.

---

## Infrastructure Layer

| Component | Purpose |
|------------|----------|
| **`ScoreDisplayView`** | Unity `MonoBehaviour` implementing `IScoreDisplayView` (e.g., updating TMP text). |

---

## Composition Layer

| Component | Purpose |
|------------|----------|
| **`ScoreInstaller`** | Dependency-injection glue that wires domain aggregate, strategies, event dispatchers, use cases, and presenters together. |

---

## Collaboration Flow

1. **Gameplay → Score**  
   `IGameplayEventsDispatcher.OnRowsCleared` → `MarshalLinesClearedEvents` → `Score` aggregate updates score → `IScoreEventsDispatcher` notifies listeners.

2. **UI Update**  
   `ScoreDisplayPresenter` listens to score changes and calls `IScoreDisplayView.UpdateScore`.

3. **Persistence**  
   `ScoreMemento` captured through `IScorePersistencePort` when saving, restored when loading.

---

[← Back](../../../../../README.md)
