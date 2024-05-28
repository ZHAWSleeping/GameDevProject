![](Gamedev/Main/Characters/Player/Human/HumanIcon.png)

# Undercard Odyssey

In this upcoming platformer you must use the power of the magical headband to venture through a mysterious cavern. Make use of special cards that empower your abilities to reach new heights (or lows).

## Installation

Undercard Odyssey does not require installation. Simply download the Game from [the "Releases" tab]() for Linux, Windows or MacOS. Do note that the MacOS version is untested and may be blocked by [Gatekeeper](https://en.wikipedia.org/wiki/Gatekeeper_(macOS)). Otherwise, ensure that the executable and the `.pck` file share the same name and are located in the same directory.

### Controls

|Action|Keyboard|Controller (XBox)|
|-|-|-|
|Move / Navigate|↑ → ↓ ←|Left stick / D-Pad|
|Jump / Accept|C|A|
|Use Card|X|X|
|Cancel|X|B|
|Pause|Esc|Start / + / ☰|

## Development

The game is built using Godot 4.3-dev6 and uses .NET 8. See below for details.

### Prerequisites

- Godot 4.3-dev6 (.NET version): https://godotengine.org/download/archive/4.3-dev6/
- .NET 8: https://dotnet.microsoft.com/en-us/download
  - Linux users may use their distro's package manager
- Visual Studio Code (recommended): https://code.visualstudio.com/
  - Install the recommended extensions
- Pixelorama to edit sprites: https://github.com/Orama-Interactive/Pixelorama
- Furnace to edit sound effects: https://github.com/tildearrow/furnace

**NOTE:** When opening the project for the first time, you may have to re-build the project first:

![](doc/img/rebuild.png)

Afterwards, close the engine and revert any changes made without touching untracked files (i. e. `.godot/`):

```sh
git reset --hard
```

### Playing & Debugging

To play, simply hit F5 or the play button in the top right. Alternatively, press F6 to run just the currently open scene.

This repo also includes a launch configuration (`Play`) for VSCode. Set your breakpoints and debug with F5 by default.

![](doc/img/debug.png)

### Points of Interest

With ~120 scenes and C# scripts this project is somewhat complex. The table below lists some particularly interesting or noteworthy components:

|File|Function|
|-|-|
|[`Root.tscn`](Gamedev/Main/UI/Root.tscn)|The root scene of the project. All other scenes will be below this one.|
|[`Player.cs`](Gamedev/Main/Characters/Player/Player.cs)|The player is by far the most complex component. Be sure to check out the finite state machine that handles the movement logic.|
|[`PlayerFSM.cs`](Gamedev/Main/Characters/Player/States/PlayerFSM.cs)|This state machine handles the different states of movement that the player can be in.|
|[`LevelManager.cs`](Gamedev/Main/Level/LevelManager.cs)|The level manager tracks the player's progress throughout the session.|
|[`SaveManager.cs`](Gamedev/Main/Persistent/SaveManager.cs)|This class manages the serialisation and deserialisation of save files.|
|[`PersistentEvents.cs`](Gamedev/Main/Events/PersistentEvents.cs)|An implementation of the "event bus" pattern. It makes various events available for subscription and invokation.|
|[`PlayerSprite.tscn`](Gamedev/Main/Characters/Player/PlayerSprite.tscn)|This scene contains an abstraction that hides a state machine and other visual components of the player.|

### Structure

Below is quick overview of the projects general structure:

```
Gamedev/
└── Main
    ├── Audio
    │   ├── Effects
    │   └── UI
    ├── Characters
    │   ├── Enemies
    │   └── Player
    │       ├── Headband
    │       ├── Human
    │       ├── Slime
    │       └── States
    ├── Constants
    ├── Events
    ├── Extensions
    ├── Level
    │   ├── Level1
    │   ├── Level2
    │   └── Level3
    ├── Objects
    │   ├── Cards
    │   ├── Respawn
    │   └── Tutorial
    ├── Persistent
    ├── Rendering
    │   └── Particles
    ├── Theme
    ├── Tiles
    │   ├── Background
    │   ├── Brickwork
    │   ├── Deathzone
    │   ├── Destructible
    │   ├── Gems
    │   ├── LoadingZone
    │   └── Pickups
    │       └── PowerUps
    ├── UI
    │   ├── Buttons
    │   ├── Cards
    │   ├── Debug
    │   ├── Font
    │   ├── Menu
    │   │   ├── Level
    │   │   ├── Main
    │   │   ├── OverWorld
    │   │   └── Victory
    │   ├── Pause
    │   ├── Save
    │   ├── Scrollable
    │   └── Title
    └── Util
```