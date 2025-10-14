# Persistence Feature

Centralized save/load for session state.  
Coordinates snapshots from Gameplay & Score, persists them via `Libs.Persistence`, hydrates features on boot, and cleans up saves on lifecycle events.

- Hydrates **Main Menu** with “Continue” availability
- Hydrates **Gameplay & Score** before initialize
- Saves session state on **board state changed**
- Deletes session state on **Game Over** and **New Game**
- Uses **atomic writes** & pluggable strategies from `Libs.Persistence`

---

## Structure

- **Application**
    - `SessionStateDataAssembly` — aggregate snapshot combining `GameplaySnapshot` and `ScoreSnapshot` for unified session persistence.
    - `SaveOnGameStateChangedUseCase` — subscribes to `IGameplayEventsDispatcher.OnBoardStateChanged` and saves the combined session snapshot.
    - `HydrateGameFeaturesBeforeInitializeUseCase` — loads previously saved session and restores `Gameplay` and `Score` snapshots before initialization.
    - `HydrateMainMenuBeforeInitializationUseCase` — checks for existing save and updates main menu “Continue” availability.
    - `DeleteSessionStateSaveFileOnGameOverUseCase` — listens to `IGameplayEventsDispatcher.OnGameOver` and deletes the session save file.
    - `DeleteSessionStateSaveFileOnNewGameUseCase` — listens to `IMainMenuEventsDispatcher.OnNewGame` and deletes the session save file.
    - `DeleteSaveFileUseCase` — abstract base for deletion use cases, encapsulating common logic.
    - `PersistenceConstants` — defines static keys and identifiers (e.g., `SESSION_STATE_SAVE_KEY`).
- **Composition**
    - `ProjectContextPersistenceInstaller` — registers `PersistentDataHandler` and binds `ISaver`, `ILoader`, and `ISaveDeleter` as shared services.
    - `MainMenuPersistenceInstaller` — wires main menu hydration and deletion use cases.
    - `GameplayPersistenceInstaller` — wires gameplay hydration, saving, and deletion use cases.

[← Back](../../../../../README.md)