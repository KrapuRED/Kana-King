# KANA KING

## About Game
Kana King is an action roguelike game designed as a Japanese language learning medium. Inspired by games like HoloCure and Vampire Survivors, it adopts their fast-paced, repetitive, and challenging gameplay patterns to keep players engaged over long periods. The core mechanics of Kana King center around the Vocabulary Learning Chain Combo (VLCC), where players must correctly input sequences of Hiragana vocabulary to execute various in-game actions, such as opening chests

This Game I made for collaboration GAT (Game Application and Technology) and JPC (Japanese Popular Culture)<br>
Game Engine = Unity 6000.0.60f1

## Team Contribution
| Name | Roles | Duration |
| :---: | :---: | :---: |
| astranot09 | Game Programmer | 14 |
| JohnathanVarren | Game Programmer | 5 |
| Jannicee | Game Artist | ... |
| Jennie Aurellia | Game Designer | ... |
| Josephine Pardede | Game Designer | ... |

## My Contribution (astranot09)
- VLCC Logic (How to trigger VLCC, How to get the katakana/hiragana data from database, etc)
- VLCC Database (Whats it need, How to get the value, etc)
- Item Pickup (When the item is automate got pick up by player, what happend when got pick up by player, etc)
- Player Stat (How To Access the stat, and How to calculate the stat into damage, defense, etc)
- Artefact Logic (How to equip this artefact, What happend when got equip, How to throw away the artefact, etc)
- Wave System
- Player Attack Melee
- Player Interact
- Crate Gacha
- Implement Animation Character in Unity, Animation UI Dotween

## Key Features

### Attack
Player can attack enemy using LMB or Button at bottom left screen.

### VLCC
Player need arrange hiragana or katakana with the correct order. if success player will get item, if failed, the item will be destroyed.

### Shop
When player level up, shop will be opened, player can buy buff to help improve stats.

### Artefact
When player defeated boss or small chance by opening crate, player will get artefact, it will give good buff to player, to help player kill the enemy.

### Wave
Every wave have a lot of enemy and different enemy stat.

### Interact
Player can interact with iinteractable object using E.

### Crate
Player need to do VLCC to open crate and the loot is random with chances.

## Layer / Module Design

<img width="1842" height="1262" alt="KanaKingLayerModule drawio drawio" src="https://github.com/user-attachments/assets/b3d2db78-3eea-46ad-8525-7a720b2e23a0" />



## Modules and Features

| Name | Scene | Responsibility |
| :---: | :---: | :---: |
| Scene Controller | All Scene | Scene transitions, load screens, exit game. |
| Audio Manager | All Scene | Plays BGM/SFX globally via audio database. |
| Player Input | Gameplay | Handles input mapping for attacks, movement, and interactions. |
| Player Interact System | Gameplay | Detects interactive objects in range via IInteractable. |
| Player Attack Melee | Gameplay | Handles melee collision detection and damage calculation. |
| Wave Manager | Gameplay | Controls wave progression, enemy difficulty scaling, and wave UI events. |
| Artefact Manager | Gameplay | Manage if there is new artefact, artefact that player current have, How to trigger if there is a new artefact |
| Artefact Database | Gameplay | To See all artefact that player can get |
| Artefact Inventory | Gameplay | Manage UI when player get Artefact |
| Shop Database | Gameplay | To See all item that player can buy in the shop |
| Shop Manager | Gameplay | Controls level-up trigger, dynamic shop inventory generation, and currency transactions. |
| Database VLCC | Gameplay | To See all hiragana/katakana, meaning, romaji that player can get |
| VLCC Manager | Gameplay | Drives the core learning mechanism, checks answer validation, and communicates success/failure callbacks to external crates/systems.|
| VLCC UI | Gameplay | Manage how to VLCC will be shown |
| Item Script | Gameplay | Handles physics magnet attraction toward player via IPickup.|
## Game Flow
<img width="2502" height="1307" alt="KanaKingGameFlow drawio" src="https://github.com/user-attachments/assets/ad9719b5-6232-47ec-ae4f-9d934a664f82" />
