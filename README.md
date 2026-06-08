# Course_DP — Design Patterns & SOLID Refactoring Course

A small Unity 2D project used as **hands-on course material** for learning clean code.
It ships with intentionally "bad" monolithic scripts that you will **refactor step by
step** — first to satisfy the **SOLID** principles, then to apply **proper design
patterns**.

> The goal is not to build a game. The goal is to turn tangled, do-everything classes
> into a clean, decoupled, extensible architecture — and to understand *why* each change
> makes the code better.

- **Engine:** Unity `6000.3.14f1` (Unity 6), Universal Render Pipeline (2D).
- **Language:** C#

---

## Table of contents

1. [What's in the box](#whats-in-the-box)
2. [Learning material](#learning-material)
3. [The starting point: the "Monolith" scripts](#the-starting-point-the-monolith-scripts)
4. [Your mission](#your-mission)
5. [SOLID in one minute](#solid-in-one-minute)
6. [Suggested patterns per script](#suggested-patterns-per-script)
7. [Getting started](#getting-started)
8. [Branch workflow](#branch-workflow)
9. [Working conventions](#working-conventions)

---

## What's in the box

```
Assets/_Core/
├── _Scripts/_Original/      ← the code you will refactor (start here)
│   ├── PlayerMonolith.cs
│   ├── MonolithInteractable.cs
│   └── Spawnable.cs
├── Documents/               ← course theory
│   ├── Design_Patterns_Guide.docx        (in-depth guide, C++ & C# examples)
│   └── Design_Patterns_Infographic.png   (one-page visual summary)
├── Prefabs/  Scenes/  Animations/  Audio/  Materials/
```

Open **`Assets/_Core/Scenes/main.unity`** to see everything running.

## Learning material

Before touching code, read the theory in `Assets/_Core/Documents/`:

| File | Use it for |
| --- | --- |
| **Design_Patterns_Guide.docx** | Deep dive into six patterns — what each is for, when to use it, pros/cons, and full C++/C# examples. |
| **Design_Patterns_Infographic.png** | A quick visual cheat-sheet to glance at while you work. |

Patterns covered: **Event Bus, Singleton, Observer, State, Decorator, Composite**.

## The starting point: the "Monolith" scripts

The scripts in `_Scripts/_Original/` deliberately break good design so you have something
real to fix.

### `PlayerMonolith.cs`
A single `MonoBehaviour` that reads input, moves the player, runs the jump/air-time logic,
and handles ground detection — all inside `Update()`.
- **Smells:** input, movement and physics rules are fused together; magic conditions;
  testing any one behaviour in isolation is impossible.
- **SOLID broken:** Single Responsibility (does input *and* movement *and* jump state),
  Open/Closed (new movement abilities mean editing `Update`).

### `MonolithInteractable.cs`
A true God-object: on collision it spawns prefabs over time, plays audio, plays particles,
**and** opens a door — plus a long null-check guard clause mixing unrelated concerns.
- **Smells:** one class with five jobs; hard-coded reaction list; duplicated
  `PlayOneShot`; flags like `isHit` tracking state by hand.
- **SOLID broken:** Single Responsibility (spawning + audio + VFX + door), Open/Closed
  (adding a new reaction means editing this class), Dependency Inversion (depends on
  concrete `AudioSource`, `ParticleSystem`, specific `GameObject`s).

### `Spawnable.cs`
Almost empty today (`Disable()`), but it's where spawned-object behaviour belongs as the
design grows.

## Your mission

Refactor the `_Original` scripts in two passes. **Keep the in-game behaviour identical** —
only the structure should change.

**Pass 1 — Make it SOLID.**
- Split each monolith into focused classes, each with one responsibility.
- Depend on abstractions (interfaces) instead of concrete components.
- Remove duplicated logic and hand-rolled state flags.

**Pass 2 — Apply design patterns.**
- Introduce the patterns from the guide where they genuinely reduce coupling or
  duplication (see the table below). Don't force a pattern in just to use it — *patterns
  are tools, not goals.*

Put refactored code in a new folder (e.g. `Assets/_Core/_Scripts/Refactored/`) and leave
`_Original/` untouched so you can compare before/after.

## SOLID in one minute

| Letter | Principle | In practice |
| --- | --- | --- |
| **S** | Single Responsibility | A class should have one reason to change. |
| **O** | Open/Closed | Open for extension, closed for modification. |
| **L** | Liskov Substitution | Subtypes must be usable through their base type. |
| **I** | Interface Segregation | Many small interfaces beat one fat one. |
| **D** | Dependency Inversion | Depend on abstractions, not concretions. |

## Suggested patterns per script

These are hints, not the only answer — justify your own choices.

| Original script | Likely patterns | Why |
| --- | --- | --- |
| `MonolithInteractable` | **Event Bus / Observer**, **Strategy/Decorator** | Replace the hard-coded "spawn + audio + VFX + door" block with an event ("interactable triggered") that independent reactor components subscribe to. Make reactions composable. |
| `PlayerMonolith` | **State**, plus SRP split | Model grounded/jumping/falling as explicit states; separate an input source from the movement/physics logic. |
| Spawned objects | **Composite**, **Object Pool** | Treat groups of spawned objects uniformly; reuse instances instead of constant `Instantiate`/`Destroy`. |
| Shared services (audio, etc.) | **Singleton** *(used sparingly)* | A single access point for cross-cutting services — but prefer injected dependencies first. |

## Getting started

1. Install **Unity 6 (`6000.3.14f1`)** via Unity Hub.
2. Clone the repo and open the project folder with the Hub.
3. Open `Assets/_Core/Scenes/main.unity` and press **Play** to see the original behaviour.
4. Read the guide in `Assets/_Core/Documents/`, then start refactoring.

> Generated folders (`Library/`, `Temp/`, `obj/`, `Logs/`, …) are intentionally
> git-ignored — only project source, settings, and packages are tracked.

## Branch workflow

| Branch | Purpose |
| --- | --- |
| `main` | The course baseline / reference. |
| `practice` | Your workspace — do your refactoring exercises here. |

Both branches currently hold the same starting point. Commit your refactoring steps on
`practice` in small, well-described increments so your progression is easy to follow.

## Working conventions

- One responsibility per class; favour composition over inheritance.
- Program to interfaces; inject dependencies rather than hunting for them.
- Keep gameplay behaviour unchanged while refactoring — verify in the `main` scene.
- Small, focused commits with clear messages.
