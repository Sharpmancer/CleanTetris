# Scene Management Library

Lightweight abstraction over Unity's `SceneManager` that allows features to trigger scene transitions using **strongly-typed arguments**, without referencing scene names directly.

---

## Contents

| Component | Description |
|------------|-------------|
| **[`ISceneManager`](ISceneManager.cs)** | Main interface for scene changes. Supports both generic and argument-based overloads. |
| **[`UnitySceneManager`](UnitySceneManager.cs)** | Concrete implementation using Unity’s built-in `SceneManager.LoadScene`. |
| **[`SceneManagerInstaller`](SceneManagerInstaller.cs)** | Registers `ISceneManager` in the Bootstrap context for global access. |
| **[`LoadSceneArgs`](LoadSceneArgs.cs)** | Base abstract class representing typed scene-load arguments. Each scene defines its own subclass. |
| **[`GameplayLoadSceneArgs`](GameplayLoadSceneArgs.cs)** | Typed argument for loading the `Gameplay` scene. |
| **[`MainMenuLoadSceneArgs`](MainMenuLoadSceneArgs.cs)** | Typed argument for loading the `MainMenu` scene. |

---

[← Back](../../../../../README.md)