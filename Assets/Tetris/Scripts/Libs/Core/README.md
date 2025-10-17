# Core Library

Low-level abstractions, lifecycle contracts, and primitive data structures shared across features and systems.  
Serves as a foundation layer, defining common behavioral patterns and value types reused throughout the project.

---

## Structure

| Category | Description |
|-----------|--------------|
| **Lifecycle** | Interfaces defining initialization and update contracts for objects that participate in the game loop. |
| **Patterns** | Reusable behavioral patterns implemented as abstractions — currently includes **Memento** (state persistence) and **Snapshot** (state serialization). |
| **Primitives** | Lightweight, allocation-free structs and value types used by multiple features. |

---

## Contents

### 🔹 Lifecycle
| File               | Purpose                                                                                                                        |
|--------------------|--------------------------------------------------------------------------------------------------------------------------------|
| `IInitializable`   | Defines an object that requires an explicit initialization step.                                                               |
| `IPreInitializable` | Defines an object that should perform a pre-initialization step before standard initialization (e.g., dependency preparation). |
| `ITickable`        | Defines an object updated every frame or tick.                                                                                 |
| `IDisposable`      | Is not defined. Indtended to be used from System namespace.                                                                    |

### 🔹 Patterns
#### Memento
| File | Purpose |
|------|----------|
| `IMemento` | Marker interface representing a captured internal state of an object. |
| `IMementoConsumer` | Defines a type capable of restoring its state from a provided memento. |
| `IMementoProvider` | Defines a type capable of producing a memento representing its current state. |

#### Snapshot
| File | Purpose |
|------|----------|
| `ISnapshot` | Represents a serialized snapshot of runtime state data. |
| `ISnapshotable` | Defines a type capable of producing and applying snapshots. |
| `MementoToSnapshotAdapter` | Adapter bridging the Memento and Snapshot patterns to unify state persistence workflows. |

### 🔹 Primitives
| File | Purpose |
|------|----------|
| `UpToFourBytes` | Compact struct for efficiently storing or transmitting up to 4 bytes of data (e.g., cleared row indices, small flags). |

---

## Usage

### Common lifecycle contracts
```csharp
internal class GameplayMediator : IPreInitializable, IInitializable, ITickable, IDisposable
```
```csharp
internal class LifecycleControlledObjects
{
    private readonly List<IPreInitializable> _preInitializables = new();
    private readonly List<IInitializable> _initializables = new();
    private readonly List<ITickable> _tickables = new();
    private readonly List<IDisposable> _disposables = new();
    
    internal void Add(object instance)
    {
        if(instance is IPreInitializable preInitializable)
            _preInitializables.Add(preInitializable);
        if(instance is IInitializable initializable)
            _initializables.Add(initializable);
        if(instance is ITickable tickable)
            _tickables.Add(tickable);
        if(instance is IDisposable disposable)
            _disposables.Add(disposable);
    }
        
    internal void PreInitialize()
    {
        foreach (var initializable in _preInitializables.OrderBy(x => x.PreInitOrder)) 
            initializable.PreInitialize();
    }
    
    internal void Initialize()
    {
        foreach (var initializable in _initializables) 
            initializable.Initialize();
    }

    internal void Tick(float timeDelta)
    {
        foreach (var tickable in _tickables) 
            tickable.Tick(timeDelta);
    }

    internal void Dispose()
    {
        // disposing in reverse-init order
        for (var i = _disposables.Count - 1; i >= 0; --i) 
            _disposables[i].Dispose();
    }
}


```
