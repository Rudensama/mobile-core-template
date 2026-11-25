# Mobile Core Template (Unity)

A clean starting point for 2D mobile puzzle games in **Unity 6** (6000.4, URP 2D, new Input System). It includes a working hex-grid core, so a new hex puzzle prototype can start from playable systems instead of an empty project.

## What's in it

**Base classes** (`Assets/Scripts/BaseClasses`)
- `Singleton<T>`: generic MonoBehaviour singleton used by all managers.
- `HexNode`, `HexNodeGroup`: hex cells and draggable groups of cells.
- `Line`: line logic on the hex grid.

**Managers** (`Assets/Scripts/Managers`), each a `Singleton`
- `GameManager`: game flow
- `StateManager`: game states
- `GridManager`: hex grid creation and queries
- `SlotManager`: the tray of pieces the player places
- `InputManager`: touch and mouse input
- `UIManager`, `AudioManager`

**Data** (`Assets/Scripts/ScriptableObjects`, `Others`)
- `HNGData`: ScriptableObject that defines piece shapes, so new pieces are added as assets, not code.
- `Other_HNGD_Generator`: editor helper that generates piece data.
- Shared enums, structs, constants and static helpers.

## Packages

URP 2D, Input System, 2D Animation, Aseprite and PSD importers, Tilemap Extras, Timeline, Test Framework.

## Run it

Open the folder in **Unity 6000.4.3f1** or newer and open `Scene_Preload` or `Scene_Main` in `Assets/Scenes`.

## Related

The same hex puzzle idea shipped in Godot as **HexaPress** on [Google Play](https://play.google.com/store/apps/details?id=rudensama.game.hexapress).

---
Made by **Sod-Erdene Byambatogtokh** (Rudensama) · [Portfolio](https://rudensama.github.io)
