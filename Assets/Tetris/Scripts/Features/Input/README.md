# Input Feature

Centralized input system that converts raw keys into **game-level commands**, with **repeat/hold** handling and **mash resolution**.

## Structure

- **Application**
    - `ProcessInputEventsUseCase` — main coordinator; polls adapter and pushes **Outbound** commands.
    - `InboundCommands` + `IInboundCommandsPort` — port used by an infrastructure layer driving adapter.
    - `IOutboundInputCommandDispatcher` — where translated commands are published (consumed by Gameplay app).
    - Strategies:
        - `IRepeatHeldInputStrategy` (`ConfigurableRepeatInputStrategy`)
        - `IResolveInputStrategy` (`LastPressedWinsResolveStrategy`)
        - `IRepeatInputStrategyConfig` — intervals for initial delay / repeat rate.
    - `IInputStateResetter` — API to clear buffered/repeat state (used when new shape spawns).

- **Infrastructure**
    - `UnityInputAdapter` — reads and pushes Unity input (keys).
    - `InputConfig` — ScriptableObject with bindings & repeat config.
    - `IInputCommandsToKeyCodesMapping` — mapping abstraction.

- **Composition**
    - `InputInstaller` — wires adapter + strategies + use case; exposes `IOutboundInputCommandDispatcher`.


[← Back](../../../../../README.md)