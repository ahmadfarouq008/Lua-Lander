# 🚀 Lua Lander - Unity 2D Beginner Project

> Learning Unity 6.5 by building a complete 2D physics-based lunar landing game. Following Code Monkey's "Learn Unity 2D - Complete Beginner Course 2026".

[![Unity](https://img.shields.io/badge/Unity-6000.2%20%2F%206.5-black?logo=unity)](https://unity.com/)
[![URP](https://img.shields.io/badge/Render%20Pipeline-URP%202D-blue)](https://docs.unity3d.com/Packages/com.unity.render-pipelines.universal@latest)
[![Language](https://img.shields.io/badge/C%23-Code-green?logo=csharp)](https://learn.microsoft.com/en-us/dotnet/csharp/)
[![Course](https://img.shields.io/badge/Course-Code%20Monkey-orange)](https://www.youtube.com/watch?v=nGKd4yTP3M8)

### 📖 About The Project
This is not a tutorial copy-paste. I am documenting my journey from zero to a playable Unity 2D game, with clean Git history and incremental features.

**Goal:** Build a portfolio-ready 2D game and understand core Unity architecture.

### 🛠 Tech Stack
- **Engine:** Unity 6.5 (6000.2) - URP 2D Renderer
- **Language:** C#
- **Core Systems:** 2D Physics (Rigidbody2D, BoxCollider2D), New Input System, Volume Framework
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
- Keep parent scale at (1,1,1)

### 🚀 Live Demo
<img width="100%" alt="Lander Physics - Rigidbody2D + Bloom" src="https://github.com/user-attachments/assets/8ae7d51e-3bc7-4af8-a36d-1f3f495517a9" />

*Lander falling with gravity, colliding with floor. Logic/Visual separation + Bloom + Input System ready.*

#### ✅ Part 4: C# Basics, Player Input [01:28:00]
- `Update()` = every frame, `Start()` = once
- `private` vs `public`, explicit private for clean code
- New Input System: `using UnityEngine.InputSystem;`, `Keyboard.current.upArrowKey.isPressed`
- Must enable Input System in Package Manager + Project Settings

#### ✅ Part 5: Physics Control, Lander Movement [01:45:00]
- `FixedUpdate()` for physics (50/sec fixed), `Update()` for input/visuals would be wrong
- `AddForce(transform.up)` = thrust where nose points, not `Vector3.up` (world up)
- `AddTorque()` = spin: +100 = left, -100 = right
- `Time.deltaTime` = same speed on all PCs, frame-independent
- Cache Rigidbody in `Awake()` not `Update()` - performance
- Magic numbers bad -> use `[SerializeField] private float force = 700f;` to tweak in Inspector
- Linear Damping 0.7 + Angular Damping 5 = stops infinite spin, makes control playable

> **Current State:** Lander fully controllable with physics. Thrust + rotation working. Ready for terrain and landing logic.

### 🚀 Live Demo
<img width="480" height="346" alt="Image" src="https://github.com/user-attachments/assets/cdd6d676-74e7-4bea-ba26-008bbeede528" />

*Lander with full physics control - Thrust where tip/nose points + rotation. FixedUpdate + Damping.*

### 🎮 Features Implemented
- [x] URP 2D Project Setup in Unity 6.5
- [x] Import Assets & Post Processing (Bloom, Vignette)
- [x] Lander Creation - Logic/Visual Separation
- [x] New Input System Setup
- [x] Lander Thrust & Rotation Physics - AddForce + AddTorque + Damping
- [ ] Terrain with SpriteShape
- [ ] Landing Detection & Crash Logic
- [ ] UI, Fuel, Coins, Levels

### 🕹 Controls
- **Up Arrow / W** - Thrust forward (where nose points)
- **Left Arrow / A** - Rotate left (counter-clockwise)
- **Right Arrow / D** - Rotate right (clockwise)

### 📁 Project Structure
