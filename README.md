# 🚀 Lua Lander - Unity 2D Beginner Project

> Learning Unity 6.5 by building a complete 2D physics-based lunar landing game. Following Code Monkey's "Learn Unity 2D - Complete Beginner Course 2026".

[[Unity](https://img.shields.io/badge/Unity-6000.2%20%2F%206.5-black?logo=unity)](https://unity.com/)
[[URP](https://img.shields.io/badge/Render%20Pipeline-URP%202D-blue)](https://docs.unity3d.com/Packages/com.unity.render-pipelines.universal@latest)
[[Language](https://img.shields.io/badge/C%23-Code-green?logo=csharp)](https://learn.microsoft.com/en-us/dotnet/csharp/)
[[Course](https://img.shields.io/badge/Course-Code%20Monkey-orange)](https://www.youtube.com/watch?v=nGKd4yTP3M8)

### 📖 About The Project
This is not a tutorial copy-paste. I am documenting my journey from zero to a playable Unity 2D game, with clean Git history and incremental features.

**Goal:** Build a portfolio-ready 2D game and understand core Unity architecture.

### 🛠 Tech Stack
- **Engine:** Unity 6.5 (6000.2) - URP 2D Renderer
- **Language:** C#
- **Core Systems:** 2D Physics (Rigidbody2D, BoxCollider2D, PolygonCollider2D), SpriteShape, Cinemachine 3.x, Sorting Layers, New Input System, Volume Framework
- **Tools:** Git, Git LFS, GitHub

### 🧠 Key Learnings So Far

#### ✅ Part 1: Create Project [00:18:00]
- Unity 6 vs 6.5 versioning & URP 2D Template
- Editor Layout, Console setup, Game View VSync

#### ✅ Part 2: Unity Basics [00:30:41]
- GameObject = Container, Component = Behavior
- Transform is mandatory, Start() / Update() lifecycle

#### ✅ Part 2.5: Import Assets, Post Processing [00:45:31]
- Import `.unitypackage`, Global Light, Global Volume, Bloom + Vignette

#### ✅ Part 3: Create Lander [01:05:00]
- Logic/Visual separation: Parent (1,1,1) = Rigidbody2D + BoxCollider2D, Child = SpriteRenderer
- Collider smaller than sprite for better game feel

### 🚀 Live Demo
<img width="100%" alt="Lander Physics - Rigidbody2D + Bloom" src="https://github.com/user-attachments/assets/8ae7d51e-3bc7-4af8-a36d-1f3f495517a9" />

*Lander falling with gravity, colliding with floor. Logic/Visual separation + Bloom + Input System ready.*

#### ✅ Part 4: C# Basics, Player Input [01:28:00]
- `Update()` = every frame, `Start()` = once
- New Input System: `Keyboard.current.upArrowKey.isPressed`
- Must enable Input System in Package Manager + Project Settings

#### ✅ Part 5: Physics Control, Lander Movement [01:45:00]
- `FixedUpdate()` for physics, `AddForce(transform.up)` where nose points
- `AddTorque()` for rotation, `Time.deltaTime` for frame independence
- Cache Rigidbody in `Awake()`, use `[SerializeField]` not magic numbers
- Linear Damping 0.7 + Angular Damping 5 = playable control

### 🚀 Live Demo
<img width="480" height="346" alt="Image" src="https://github.com/user-attachments/assets/cdd6d676-74e7-4bea-ba26-008bbeede528" />

*Lander with full physics control - Thrust where tip/nose points + rotation. FixedUpdate + Damping.*

#### ✅ Part 6: Terrain Sprite Shape [02:05:00]
- SpriteShape = draw any shape with spline points, auto-tiles texture - perfect for hills + flat landing pads
- Install 2D SpriteShape package from Package Manager
- SpriteShapeController = holds spline + profile, Edit Spline button to add points
- Need `PolygonCollider2D` + Auto Update Collider = ON, else lander falls through
- Profile has **Fill** (inside dirt texture) + **Border** (edge/grass texture)
- **Pixels Per Unit** controls texture size - lower = bigger texture, higher = smaller/tiled
- **Angle Ranges** = grass on top (0-180) and dirt on sides (180-360) for realistic terrain
- Flat points = landing pads, angled points = crash zones
- Keep terrain as Closed Shape + collider IsTrigger = OFF

#### ✅ Part 7: Cinemachine Camera Follow [02:20:00]
- Unity 6.5 uses **Cinemachine 3.x** - called **Cinemachine Camera** (not Virtual Camera)
- Install from Package Manager > Cinemachine, Main Camera gets **Cinemachine Brain**
- Create **Cinemachine Camera** + **Cinemachine Position Composer** extension
- Set **Tracking Target** = Lander, camera now follows
- DeadZone inside Position Composer - Width/Height 0.1 = no jitter on small hover
- Lens > Orthographic Size controls zoom - don't edit Main Camera, it gets overwritten

### 🚀 Live Demo
<img width="480" height="214" alt="Image" src="https://github.com/user-attachments/assets/6a4a42d3-d415-40fd-8ec4-4ce1f8495297" />

#### ✅ Part 8: Background, Sorting Order [02:35:00]
- **Sorting Layer > Sorting Order** - Layer wins first, then Order inside layer
- Sorting Layers list: Top = behind, Bottom = front. Created new **Background** layer on top
- Background: Layer = `Background`, Order = `0`, Draw Mode = `Tiled`, Size = 40x25 (not 500x500 = lag)
- Terrain: Layer = `Default`, Order = `0` - Default is below Background, so always in front of background
- Lander: Layer = `Default`, Order = `10` - Same layer as terrain but higher Order = in front
- Final stack: Background (Background,0) -> Terrain (Default,0) -> Lander (Default,10)
- Why own layer for background? Guarantees it stays behind even if Default Orders change, no z-fighting
- Pixels Per Unit = how many pixels = 1 unit - low PPU = huge tiles, high PPU = tiny tiles
- Z position ignored in URP 2D - only Sorting Layer + Order matters, keep all at Z=0

#### ✅ Part 9: Landing Detection & Crash Logic [02:50:00]
- **Physics callbacks:** `OnCollisionEnter2D(Collision2D collision)` for solid hits, `OnTriggerEnter2D` for triggers - must have `IsTrigger = OFF` for collision
- **Requirements for collision:** Both objects need `Collider2D` + one needs `Rigidbody2D` `Dynamic`. If either is `IsTrigger = ON`, collision never fires
- **Speed check:** `collision.relativeVelocity.magnitude` = impact speed. `> 4f` = too hard / crash, `< 4f` = soft / success
- **Spelling matters:** `OnCollisionEnter2D` not `OnCollisioEnter2D` - VS Code shows `0 references` if Unity doesn't recognize it = method never called
- **Location:** Callback must be inside `Lander` class but outside `FixedUpdate()`, attached to Lander GameObject that has Rigidbody2D

> **Current State:** Landing detection working with speed check - hard landing vs soft landing logs in Console. Ready for landing pads, crash effects and game states.

### 🎮 Features Implemented
- [x] URP 2D Project Setup in Unity 6.5
- [x] Import Assets & Post Processing (Bloom, Vignette)
- [x] Lander Creation - Logic/Visual Separation
- [x] New Input System Setup
- [x] Lander Thrust & Rotation Physics - AddForce + AddTorque + Damping
- [x] Terrain with SpriteShape + PolygonCollider2D + Angle Ranges
- [x] Cinemachine Camera Follow - Cinemachine Camera + Position Composer + Tracking Target + DeadZone
- [x] Background Tiling + Sorting Order - Background Layer (0) / Default Terrain (0) / Default Lander (10)
- [x] Landing Detection & Crash Logic - OnCollisionEnter2D + relativeVelocity.magnitude + Debug.Log
- [ ] Landing Pad Tag Check, UI, Fuel, Coins, Levels

### 🕹 Controls
- **Up Arrow / W** - Thrust forward (where nose points)
- **Left Arrow / A** - Rotate left
- **Right Arrow / D** - Rotate right

### 📁 Project Structure
