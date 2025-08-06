# Multiplayer Item Pickup System (Unity + Protobuf + Netcode)

This project demonstrates a robust multiplayer item pickup system using Unity and Protobuf where two players may attempt to pick up the same item at the same time — and **only one** will succeed.

---

## Goal

Ensure consistent behavior in multiplayer games when multiple players attempt to pick up the same item simultaneously. Prevent item duplication and race conditions by syncing via the server/backend.

---

## Technologies & Assets Used

### Unity Frontend
- **Engine:** Unity 2022 or later
- **Netcode:** Unity Netcode for GameObjects
- **Serialization:** Google Protobuf
- **Assets:**
  -  **Low Poly Simple Nature Pack** (Environment)
  -  **Human Basic Motions** (Characters)
  -  **Food Props** (Pickable Items)

###  Backend
- Protobuf for item pickup serialization
- Netcode for gameobjects for networking

---

## Features

- Players can walk around and interact with the world.
- Pickable items are synced across clients.
- When two players try to pick the same item, only the **first confirmed** request is granted.
- Items disappear once picked up (visually synced).
- Conflict safe logic on the server.

---

### Item Pickup Flow

1. Player enters range of an item.
2. Player presses the **interact key** (`E`).
3. Unity serializes a `PickupRequest` message via **Protobuf** and sends it to the server.
4. Server validates:
   - Has the item already been picked up?
   - Is this the first valid request?
5. If successful:
   - Server broadcasts `PickupConfirmed` to all players.
   - Clients remove the item from the scene.
6. If failed:
   - Server sends `PickupDenied` to the player.

---

