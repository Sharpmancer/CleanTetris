# Core Library

Low-level abstractions and value types shared across features and systems.  
Defines common contracts (e.g., lifecycle, ticking, events, data structs) that form the foundation of higher-level features.

---

## Contents

| Component | Purpose                                                                                    |
|------------|--------------------------------------------------------------------------------------------|
| `IInitializable` | Defines objects that require an explicit initialization step.                              |
| `ITickable` | Defines objects updated every frame (or tick).                                             |
| `IDisposable` | Is not defined here, but intended to be used alongside `IInitializable`                    |
| `UpToFourBytes` | Compact struct for efficient transmission of up to 4-byte data (e.g. cleared rows' indices) | 
---

## Usage

Establish common lifecycle across the project
```csharp
public class GameplayMediator : IInitializable, ITickable, IDisposable
```

[← Back](../../../../../README.md)