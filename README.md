# 🚀 Lua Lander - Unity 2D Beginner Project

> Learning Unity 6.5 by building a complete 2D physics-based lunar landing game. Following Code Monkey's "Learn Unity 2D - Complete Beginner Course 2026".

[![Unity](https://img.shields.io/badge/Unity-6000.2%20%2F%206.5-black?logo=unity)](https://unity.com/)
[![URP](https://img.shields.io/badge/Render%20Pipeline-URP%202D-blue)](https://docs.unity3d.com/Packages/com.unity.render-pipelines.universal@latest)
[![Language](https://img.shields.io/badge/C%23-Code-green?logo=csharp)](https://learn.microsoft.com/en-us/dotnet/csharp/)
[![Course](https://img.shields.io/badge/Course-Code%20Monkey-orange)](https://www.youtube.com/watch?v=nGKd4yTP3M8)

### 🚀 Live Demo
<img width="100%" alt="Lander Physics - Rigidbody2D + Bloom" src="https://github.com/user-attachments/assets/8ae7d51e-3bc7-4af8-a36d-1f3f495517a9" />

*Lander falling with gravity, colliding with floor. Logic/Visual separation + Bloom + Input System ready.*

### 📖 About The Project
This is not a tutorial copy-paste. I am documenting my journey from zero to a playable Unity 2D game, with clean Git history and incremental features.

**Goal:** Build a portfolio-ready 2D game and understand core Unity architecture.

### 🛠 Tech Stack
- **Engine:** Unity 6.5 (6000.2) - URP 2D Renderer
- **Language:** C#
- **Core Systems:** 2D Physics (Rigidbody2D, BoxCollider2D), New Input System, Volume Framework, Post Processing
- **Tools:** Git, Git LFS, GitHub

### 🧠 Key Learnings So Far

#### ✅ Part 1: Create Project [00:18:00]
- Unity 6 vs 6.1 vs 6.5 versioning & URP 2D Template
- Editor Layout, Console setup (Clear on Play, Error Pause, Collapse)
- Game View VSync & Aspect Ratio settings

#### ✅ Part 2: Unity Basics [00:30:41]
- GameObject = Container, Component = Behavior
- Transform is mandatory, Hierarchy / Project / Inspector
- Scene vs Game View, Lifecycle: Start() / Update()
- Parent-Child, Local vs World Position, Debug.Log

#### ✅ Part 2.5: Import Assets, Post Processing [00:45:31]
- How to import `.unitypackage` (Custom Package)
- Why sprite might not show: Z!= 0, Global Light intensity 0, Sorting Layer
- 2D Global Light intensity controls brightness for all lit sprites
- Global Volume = post processing for whole scene (Is Global = true)
- Volume Profile = asset that holds Bloom, Vignette overrides
- Camera must have Post Processing enabled to see effects
- Bloom = glow on bright pixels, Vignette = dark edges for focus

#### ✅ Part 3: Create Lander [01:05:00]
- Logic/Visual separation pattern: Parent (1,1,1) = Rigidbody2D + BoxCollider2D, Child = SpriteRenderer
- Rigidbody2D for gravity, BoxCollider2D for collision shape
- Collider slightly smaller than sprite = better game feel
- Never mix 2D and 3D physics
- Keep logic parent scale at (1,1,1) to avoid physics bugs

#### ✅ Part 4: C# Basics, Player Input [01:28:00]
- `Update()` = runs every frame, `Start()` = called once
- `private` vs `public`: private = only this class, public = other scripts can access
- Explicit `private` for Start/Update = clean code, encapsulation
- Unity can call private Start/Update via reflection
- `Debug.Log()` not showing? Script not attached, object inactive, Collapse ON
- Remove empty Start() to save performance
- New Input System vs Old Input Manager: Old = `Input.GetKey()`, New = `Keyboard.current.key.isPressed`
- Need `using UnityEngine.InputSystem;` namespace
- Must enable New Input System in Project Settings + Package Manager
- `Keyboard.current.upArrowKey.isPressed` = checks if Up Arrow held
- Attach logic scripts to parent not child
- VSync OFF in Game View during dev

> **Current State:** Lander with physics + Input System setup complete. Reading arrow input. Ready to add thrust forces.

### 🎮 Features Implemented
- [x] URP 2D Project Setup in Unity 6.5
- [x] Editor & Console Configuration
- [x] Import Free Assets & Setup Post Processing
- [x] Lander Creation - Rigidbody2D + BoxCollider2D
- [x] Floor Collision Setup
- [x] C# Basics - Access Modifiers, Start/Update Lifecycle
- [x] New Input System Setup & Keyboard Input Reading
- [ ] Lander Thrust & Rotation Physics (Next)
- [ ] Terrain with SpriteShape
- [ ] Landing Detection & Crash Logic
- [ ] UI, Fuel, Coins, Levels

### 🕹 Controls
- **Up Arrow / W** - Thrust (coming next)
- **Left/Right Arrow / A/D** - Rotate (coming next)

### 📁 Project Structure
Assets/
├── _Project/Scenes/SampleScene
├── CodeMonkey/Free Lander Assets
└── Scripts/
    ├── Testing.cs
    └── Lander.cs - Input reading with new Input System

### 🚀 How to Run
1. Clone repo
2. Open with Unity 6000.2+
3. Open SampleScene - Press Play

### 🗺️ Next Up
- **Part 5: Lander Movement** -> AddForce for thrust, AddTorque for rotation

### 👨‍💻 Author
**Ahmad Farooq** - Aspiring Unity Developer
