# Quest VR Gorilla Locomotion Prototype

This repository contains a Unity starter project scaffold for a Meta Quest VR game inspired by Gorilla Tag locomotion, enemy encounters, character transformations, and soul-block gacha mechanics.

Important: This is not a final, QA-tested, production-ready APK generator. A real Quest APK still requires a PC running Unity with Android build support and the Meta XR tools installed. The code in this repo is a workable prototype foundation you can build from.

## Included systems

- Gorilla Tag-style climbing/swinging locomotion
- VR controller input handling
- Basic enemy AI targeting nearest player
- Enemy spawning and attack system
- Soul block collectible and dragon transformation mechanic
- Character shell system with gun/web shooters
- Game manager and flow glue

## Project layout

- `Assets/Scripts/VR`  - locomotion and controller logic
- `Assets/Scripts/Enemy` - AI, attack, spawn
- `Assets/Scripts/Gacha` - soul blocks and dragon logic
- `Assets/Scripts/Abilities` - transformable character powers
- `Assets/Scripts/Core` - shared health and game management

## How to use

1. Install Unity 2022 LTS or newer.
2. Install Android Build Support.
3. Install Meta XR / Oculus packages.
4. Open this folder in Unity.
5. Add the required XR packages if prompted.
6. Build to Android for Quest 2/3/Pro.

## Quick build flow

- Open Unity
- File > Build Settings > Android
- Enable Android target
- Add your scene(s)
- Player Settings: set Android package name and XR settings
- Build APK
- Sideload to Meta Quest with ADB

## Important note

This repo gives you a strong base, but you still need to do the actual Unity setup and APK compilation on a computer. GitHub and Unity Cloud are useful for managing the project, but no phone can compile a proper Quest APK by itself.

## Suggested next steps

1. Open the project in Unity.
2. Create the actual scene and assign references in Inspector.
3. Add your own player rig, dragon model, and enemies.
4. Build and test in headset.

---

This starter code is intended as a practical prototype foundation, not a finished commercial game.
