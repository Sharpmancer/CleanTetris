# Main Menu Feature

Entry UI and scene-flow hub. Triggers game start and exposes simple menu actions.

## Structure

- **Application**
    - `HandleMainMenuCommandsUseCase` — reacts to menu actions (start game, exit, etc.) and coordinates scene flow.

- **Infrastructure**
    - `MainMenuDialogue` — Unity-side view that captures button presses and forwards them via `IMainMenuDialogue`.

- **Composition**
    - `MainMenuInstaller` — binds `IMainMenuDialogue` to `MainMenuDialogue`, wires `HandleMainMenuCommandsUseCase`.

[← Back](../../../../../README.md)