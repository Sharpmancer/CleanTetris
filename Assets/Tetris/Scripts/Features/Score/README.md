# Score Feature

Independent scoring system that listens to gameplay's **rows-cleared** events and updates score (and UI) accordingly.

## Structure

- **Domain**
    - `ScoreTracker` — encapsulates current score and scoring rules; raises events via `IScoreEventsDispatcher`.
    - `ILinesClearedHandler` — contract to handle rows-cleared events.
    - `IScoreConfig` — scoring configuration (per-lines-cleared table, level scaling, etc.).

- **Application**
    - `MarshalLinesClearedEvents` — subscribes to `IGameplayEventsDispatcher.OnRowsCleared` and calls `ScoreTracker`.
    - `ScoreDisplayPresenter` — listens to score changes and updates `IScoreDisplayView`.

- **Infrastructure**
    - `ScoreConfig` — ScriptableObject implementing `IScoreConfig`.
    - `ScoreDisplayView` — UI surface (e.g., TMP label) implementing `IScoreDisplayView`.

- **Composition**
    - `ScoreInstaller` — binds `ScoreTracker`, loads `ScoreConfig`, wires `MarshalLinesClearedEvents` and presenter.

[← Back](../../../../../README.md)