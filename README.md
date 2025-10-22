# CleanTetris

A Tetris implementation built to demonstrate **Domain-Driven Design (DDD)** and **layered architecture** principles in Unity/C#.

The goal of this project is to capture the essence of these architectural ideas and test their **practicality and synergy** in a small but complete system.  
An ideal project of this type should achieve the following qualities:

- **Ease of mental mapping** — no guesswork about where to find or place functionality.  
  *(Example: Features → Playfield → App → Api → OnRowsCleared event)*

- **Streamlined dependency structure** — only the Application layer can depend on other features;  
  Domain is always isolated, and Infrastructure / Composition remain self-contained within each feature.

- **Smooth iteration** — features, algorithms, or rules can be swapped via the Composition layer  
  without ever modifying any business logic. *(Example: `PureRandom` and `BagOf7` shape-spawn strategies are injected interchangeably based on a `SpawnAlgorithm` enum value in the Composition layer)*

---

## ⚙️ Key Design Principles

- **Dependency Rule:** Source code dependencies always point inward `Domain ← Application ← Infrastructure ← Composition`.
  Business logic never depends on implementation details or engine code.

- **Dependency Inversion:** High-level policies (Domain/App) define interfaces; low-level layers (Infrastructure) implement them.
  Both depend on abstractions, ensuring isolation and easy substitution.

- **Explicit Boundaries:** Each layer communicates only through its public API; all other types remain internal to the assembly.

- **Acyclic Dependencies (DAG):** Every feature-layer combination is isolated by its own `.asmdef` to guarantee no circular references.

- **Reactive Event Flow:** Domain raises events → Application dispatches them → Presentation updates/other features react to changes.

- **Dependency Injection:** All dependencies are resolved in the Composition layer via hierarchical contexts, ensuring clear ownership and lifetimes.

- **Vertical + Horizontal Slicing:** The project is organized vertically by **features** and horizontally by **layers**, separating game systems from each other and engine integration from domain logic respectively.

- **Shared Kernel & Reusability:** Game-agnostic subsystems (e.g., Bitmask, DI, Core abstractions) live in independent libraries following SoC and reuse principles.

## 🏗️ Layers

### Domain
- Defines the **core feature model**, encapsulating its data structures, behaviour and state.
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

| Feature | Description | Layers                                |
|---|---|---------------------------------------|
| [Playfield](Assets/Tetris/Scripts/Features/Playfield/README.md) | Core Tetris loop — board, shapes, gameplay state machine. | **D** ✅ · **A** ✅ · **I** ✅ · **C** ✅ |
| [Score](Assets/Tetris/Scripts/Features/Score/README.md) | Independent scoring reacting to gameplay events. | **D** ✅ · **A** ✅ · **I** ✅ · **C** ✅ |
| [Input](Assets/Tetris/Scripts/Features/Input/README.md) | Converts button presses into commands with repeat/anti-mash. | **D** ❌ · **A** ✅ · **I** ✅ · **C** ✅ |
| [MainMenu](Assets/Tetris/Scripts/Features/MainMenu/README.md) | Entry UI and scene-flow hub. | **D** ❌ · **A** ✅ · **I** ✅ · **C** ✅ |
| [Persistence](Assets/Tetris/Scripts/Features/Persistence/README.md) | Saving, loading, hydration, cleanup across features. | **D** ❌ · **A** ✅ · **I** ❌ · **C** ✅  |

*Legend: **D** = Domain, **A** = Application, **I** = Infrastructure, **C** = Composition*
## 🧰 Shared Libraries

| Library | Description |
|----------|--------------|
| [Core](Assets/Tetris/Scripts/Libs/Core/README.md) | Common abstractions and interfaces (`IInitializable`, `ITickable`, etc.). |
| [Bitmasks](Assets/Tetris/Scripts/Libs/Bitmasks/README.md) | Utility for efficient 1D and 2D bitmask operations. |
| [Bootstrap](Assets/Tetris/Scripts/Libs/Bootstrap/README.md) | Dependency Injection setup and installer base types. |
| [SceneManagement](Assets/Tetris/Scripts/Libs/SceneManagement/README.md) | Scene transition helpers. |
| [OneBitDisplay](Assets/Tetris/Scripts/Libs/OneBitDisplay/README.md) | Minimalistic grid-based pixel display renderer. |
| [Persistence](Assets/Tetris/Scripts/Libs/Persistence/README.md) | Modular, strategy-based save/load system supporting serialization, encryption, and atomic file operations. |
| [UnityUtils](Assets/Tetris/Scripts/Libs/UnityUtils/README.md) | Making life easier. |

---

## License
MIT
