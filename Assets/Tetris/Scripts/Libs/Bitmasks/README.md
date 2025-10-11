# Bitmasks Library

A high-performance, allocation-free library for compact **bitwise representations** of 2D grids and flag sets.  
Used across features to handle board states, shape occupancy, and flag operations efficiently — fully engine-agnostic.

---

## Contents

| Component | Description |
|------------|-------------|
| **`BitMask2D`** | Compact 2D boolean grid using bitmasks; each row stored as a 32-bit `uint`. Implements `IReadOnlyBitMask2D`. |
| **`IReadOnlyBitMask2D`** | Read-only contract for exposing a 2D bitmask safely. Useful for visualizers and domain queries. |
| **`BitOperations`** | Utility with `TrailingZeroCount(uint)` — counts the index of the lowest set bit efficiently. |
| **`Int64Extensions`** | Bitwise helpers for `long` masks (`HasExactlyOneFlag`, `ContainsFlag`, `RemoveFlags`, etc.). |
| **`EnumBits`** | Converts between `Enum` values and their raw bit patterns, supporting generic flag-based enums. |
| **`FlagsExtensions`** | High-level extensions for `Enum` flags (e.g. `HasAnyFlags`, `AddFlags`, `FirstSetFlag`). |

---

[← Back](../../../../../README.md)
 