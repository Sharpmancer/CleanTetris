# CleanTetris

A Tetris implementation built to demonstrate **Domain-Driven Design (DDD)** and **layered architecture** principles in Unity/C#.

This project is not about gameplay complexity — it’s a **code example** showcasing how to structure even a simple game using clear boundaries, dependency inversion, and testable domain logic.

---

## ⚙️ Key Design Principles

- **Dependency Inversion:** inner layers never depend on the outer ones  
  (`Domain ← Application ← Infrastructure ← Composition`).

- **Explicit Boundaries:** each layer communicates only through dedicated interfaces and DTOs — internal types and state remain encapsulated.

- **Reactive Event Flow:** Domain raises events → Application dispatches → Presentation updates.

- **Dependency Injection:** all dependencies are resolved in Composition via hierarchically organized contexts, ensuring clear ownership and lifetimes.

- **Vertical + Horizontal Slicing:** the project is divided vertically by independent features and horizontally by architectural layers, separating engine and business logic concerns.

- **Shared Kernel & Reusability:** common, game-agnostic mechanisms (e.g., bitmasks, di) are extracted into standalone libraries following SoC and reusability reasons.

---

## 🏗️ Layers

### Domain
- Defines the **core feature model**, incapsulating its data structures, behaviour and state.
- Exposes **interfaces** for application layer to interact with.
- Pure C#, engine-agnostic.

### Application
- Coordinates domain logic and connects it with external systems via **presenters** and **use cases**.
- Defines those external systems' interfaces.

### Infrastructure
- Provides Unity-specific implementations of domain and app abstractions intended to be handled by **an engine** (input reading, scriptable configs, UI, rendering e.t.c.).

### Composition
- Acts as the **composition root**, wires Domain, Application, and Infrastructure implementations through dependency injection.
- Defines **lifetime, initialization order, and inter-feature connections**.

---

## 🧩 Features

| Feature                                                       | Description |
|---------------------------------------------------------------|--------------|
| [Gameplay](Assets/Tetris/Scripts/Features/Gameplay/README.md) | Core Tetris loop — board, shapes and gameplay state machine. |
| [Score](Assets/Tetris/Scripts/Features/Score/README.md)                             | Independent scoring system reacting to gameplay events. |
| [Input](Assets/Tetris/Scripts/Features/Input/README.md)                             | Converts button presses into game commands with repeat timing and anti-mash logic. |
| [MainMenu](Assets/Tetris/Scripts/Features/MainMenu/README.md)                       | Entry UI and scene-flow hub. |

## 🧰 Shared Libraries

| Library | Description |
|----------|--------------|
| [Core](Assets/Tetris/Scripts/Libs/Core/README.md) | Common abstractions and interfaces (`IInitializable`, `ITickable`, etc.). |
| [Bitmasks](Assets/Tetris/Scripts/Libs/Bitmasks/README.md) | Utility for efficient 1D and 2D bitmask operations. |
| [Bootstrap](Assets/Tetris/Scripts/Libs/Bootstrap/README.md) | Dependency Injection setup and installer base types. |
| [SceneManagement](Assets/Tetris/Scripts/Libs/SceneManagement/README.md) | Scene transition helpers. |
| [OneBitDisplay](Assets/Tetris/Scripts/Libs/OneBitDisplay/README.md) | Minimalistic grid-based pixel display renderer. |
| [UnityUtils](Assets/Tetris/Scripts/Libs/UnityUtils/README.md) | Making life easier. |

---

## License
MIT
