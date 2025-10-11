# One-Bit Display Library

A minimal **1-bit rendering system** for Unity — used to efficiently display binary (on/off) pixel grids.  
Ideal for visualizing game board states, bitmask fields, or debugging procedural systems without relying on sprites or meshes.

---

## Contents

| Component | Description |
|------------|-------------|
| **`IOneBitDisplay`** | Interface defining a minimal display surface (`Initialize`, `SetPixels`).|
| **`OneBitDisplay`** | Abstract MonoBehaviour base class that implements `IOneBitDisplay`.  |
| **`OneBitTexture`** | Core 1-bit framebuffer implementation. Efficiently updates only changed pixels.|
| **`OneBitImageDisplay`** | `UI.Image`-based implementation for displaying one-bit textures in Canvas.|

---
[← Back](../../../../../README.md)