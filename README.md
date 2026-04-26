# Modular Inventory System (Unity)

A data-driven, modular inventory system built in Unity using ScriptableObject architecture and event-driven UI updates

The system is designed for scalability, reusability, and decoupling between gameplay logic and UI

---

## Key Features

- **Data-driven items** using ScriptableObjects
- **Event-driven architecture** (no tight coupling between systems)
- **Modular design** allowing easy extension of item types
- **Runtime-safe inventory operations**
- **UI decoupled from logic layer**
- Designed for reuse across multiple Unity projects

---

## Architecture Overview

The system is built around separation of concerns:

- **Item Data Layer**
  - ScriptableObjects define item properties (id, icon, stack size, etc.)

- **Inventory Logic Layer**
  - Handles adding, removing, stacking items
  - Independent from UI implementation

- **Event System**
  - Inventory changes are broadcast via C# events
  - UI listens and updates dynamically

- **UI Layer**
  - Subscribes to inventory events
  - Pure presentation layer

---

## Core Systems

### Item Definition
Items are defined using ScriptableObjects:

- Item name
- Icon
- Stack size

---

### Inventory Controller
Handles:

- Adding items
- Removing items
- Stack management

---

### UI Layer
- Fully event-driven
- Updates only when inventory state changes
- No direct polling or tight coupling to logic

---

## Design Principles Used

- SOLID principles
- Separation of concerns
- Event-driven programming
- Data-driven design

---

## Use Cases

This system can be used as a base for:

- RPG inventory systems
- Survival games
- Crafting systems
- Item-based progression systems

---

## Stack

- Unity (C#)
- ScriptableObjects
- C# Events / Actions / Interfaces
- Unity UI (UGUI)

---

## Demo

![2026-04-0918-30-34-ezgif com-crop](https://github.com/user-attachments/assets/887efb5e-613f-45c4-9b0c-1f1ab2657e8b)

---

## Notes

This system was built as a reusable gameplay module focusing on modularity and scalability rather than a single game implementation
