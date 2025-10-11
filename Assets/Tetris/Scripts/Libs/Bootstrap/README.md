# Bootstrap Library

Provides a lightweight **dependency injection and lifecycle orchestration system** for Unity.  
It wires feature-level installers into **context hierarchies**, ensuring proper initialization, ticking, and disposal of services across scenes.

---

## Contents

| Component | Description |
|------------|-------------|
| **`Installer`** | Base class for all feature installers. Each installer registers contracts and runnables into its context. |
| **`IInstallableContext`** | Interface exposing DI container operations — retrieving, registering contracts, and registering lifecycle participants. |
| **`RunnableContext`** | Core runtime container. Manages service instances and lifecycle groups (`IInitializable`, `ITickable`, `IDisposable`). Supports parent context fallback. |
| **`ContextRunner`** | Abstract MonoBehaviour that owns a `RunnableContext`. Executes lifecycle steps (`Install`, `Initialize`, `Tick`, `Dispose`) on Unity callbacks. |
| **`ProjectContextRunner`** | Global root context. Ensures only one instance exists and persists between scenes (`DontDestroyOnLoad`). |
| **`SceneContextRunner`** | Scene-level context connected to the global `ProjectContextRunner`. Runs feature installers and propagates lifecycle updates. |

---

### Unity → Bootstrap Lifecycle Mapping

| Unity Lifecycle | Bootstrap Context Lifecycle                                  |
|-----------------|--------------------------------------------------------------|
| Awake()         | Installers → Register contracts/runnables                   |
| Start()         | IInitializable.Initialize()                                  |
| Update()        | ITickable.Tick(deltaTime)                                    |
| OnDestroy()     | IDisposable.Dispose()                                        |

---

[← Back](../../../../../README.md)