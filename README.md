# Ruins Expedition

## Game Concept

You are an explorer that found some forgotten ruins reclaimed by the forest.
The ruins hold an Ancient Chest said to carry a 1000-year curse, and it can
only be opened once all the ancient symbols have been collected.

When night falls, Guardian creatures that once protected the temple wake up
and hunt anything that moves near them. These guardians only chase you at
night, so be patient and survive until daylight returns. During the day,
you're able to rest and heal any damage they caused.

## Goal

Collect all **5 Ancient Symbols** hidden around the forest and bring them to
the Ancient Chest to open it and win.

## Controls

| Action | Input |
|---|---|
| Move | WASD |
| Look | Mouse |
| Jump | Space |
| Sprint | Left Shift (hold) |
| Interact (gather / open chest) | E |
| Rest (heal, day only) | F |
| Pause / Resume | Esc |
| Restart after win/lose | Enter |

## How to Play

1. On launch, choose **New Game** (or **Continue**, if a previous save
   exists) from the start menu.
2. Explore the ruins during the day and gather the 5 Ancient Symbol rocks
   scattered around the forest — walk up to one and press E.
3. If you're hurt and it's still daytime, press F to rest and heal before
   heading back out.
4. Watch the light — when night falls, the 5 Guardian creatures wake up and
   will chase and damage you if you get close. Break line of sight/range to
   make them return to their post.
5. Once you're carrying all 5 Symbols, find the Ancient Chest and press E to
   open it and win.
6. Press Esc at any time to pause, and Quit to Menu if you want to step
   away — your progress is saved automatically, so Continue will drop you
   back in right where you left off.

## Gameplay Systems

**Day/Night Cycle** — the world cycles between a 30-second day and a
30-second night, with lighting shifting between them as the only warning
before guardians activate.

**Symbol Gathering** — 5 Ancient Symbol rocks are placed around the forest.
Walking up to one shows a prompt; pressing E collects it and updates the
on-screen counter live.

**Resting** — pressing F during the day (only shown as an option when below
full health) locks the player in place, plays a sitting animation, and
heals a small amount per tick until full health is reached or night falls,
which cancels it automatically.

**Guardian AI** — 5 creatures, each intentionally placed near a Symbol
rock. Dormant and harmless during the day. At night, they detect the player
within range, chase, and deal chip damage on contact (20% of max health per
hit). If the player breaks line of sight/range, the guardian walks back to
its original post rather than pursuing indefinitely or freezing in place.
Guardians also follow sloped terrain via ground-snapping, so they stay
grounded across uneven ruin surfaces.

**Ancient Chest** — the win trigger. Shows live progress toward the 5
Symbol requirement when interacted with, and opens once the requirement is
met.

**Health System** — a UI health bar tracks player HP. Guardian hits reduce
it; resting restores it; reaching zero ends the game in a loss state.

**Win / Lose / Restart** — a win panel appears on opening the chest, a lose
panel appears on death, and pressing Enter (or a Restart button) reloads
the level to try again.

**Minimap & Compass** — an overhead minimap follows the player, and a
heading-based compass bar helps with orientation while exploring the
ruins.

**Day/Night Music** — background music crossfades between a day track and
a night track based on the current cycle phase, with a separate menu track
playing during the start/pause menus.

### Advanced Feature: Save / Load System

The game persists progress across sessions using a JSON + `PlayerPrefs`
based save system (chosen specifically because it's reliable on WebGL
builds, where raw file I/O is unreliable in-browser):

- **Autosave** runs on a fixed interval and on quit, capturing the
  player's Symbol count, health, and current day/night phase.
- A **"Saving..."** indicator briefly appears in the UI whenever a save
  occurs.
- The **start menu** offers **Continue** (enabled only if a save exists)
  or **New Game** (clears any existing save).
- A full **Pause menu** (Esc) lets the player **Resume** or **Quit to
  Menu** — quitting saves immediately and returns to the start screen,
  and Continuing from there resumes exactly where the player left off,
  without reloading the level.
- The save automatically clears on both win and loss, so a completed run
  always starts fresh next time.

## Final Project Requirement Checklist

1. **Create an original 3D game in Unity** — Done. Built on top of the
   original class skeleton (movement, camera, resource-gathering
   framework, day/night lighting), developed into a fully original game.
2. **Playable 3D world/environment** — Done. Forest terrain with ruin
   structures placed throughout, forming the explorable space.
3. **Controllable player character** — Done. Third-person character
   controller; the player model was swapped mid-development with full
   re-wiring of movement, camera target, and grounded detection.
4. **Working camera system** — Done. Cinemachine third-person follow
   camera.
5. **5+ interactive/collectible/gameplay objects** — Done. 5 Ancient
   Symbol rocks + the Ancient Chest, plus 5 Guardian creatures as hazard
   objects.
6. **3+ interaction systems** — Done. (1) Gathering Symbols, (2) Resting
   to heal (day-only, state-gated), (3) Opening the Ancient Chest
   (progress-gated, triggers win).
7. **UI feedback** — Done. Health bar, live Symbol count, contextual
   prompts ("Press E to gather," "Press F to rest," live chest progress),
   and a "Saving..." indicator.
8. **3+ challenge systems** — Done. (1) Guardian AI hazard with
   chase/attack behavior, (2) Night time-pressure (guardians only active
   at night), (3) Resource-guarding placement — each Guardian is
   intentionally posted beside a Symbol rock.
9. **Clear gameplay goal** — Done. Collect all 5 Ancient Symbols and
   deliver them to the chest.
10. **Win/lose/restart flow** — Done. Win panel on chest completion, lose
    panel on player death, Enter key (or Restart button) reloads the
    scene to restart.
11. **Audio** — Done. Day/night background music crossfade plus separate
    menu music.
12. **Visual polish** — Done. Dynamic lighting across the day/night
    cycle, ruin props placed for environmental storytelling.
13. **Original design choices (not a copy of the class project)** — Done.
    Custom theme, win/lose systems, resting mechanic, guardian AI with
    return-to-post behavior, player model swap, and a full save/load +
    pause system beyond anything covered in class.
14. **Playable start to finish without major errors** — Done. Full loop
    confirmed playable: gather, rest, get chased/damaged by guardians,
    save/load/pause at any point, win via chest or lose via death,
    restart cleanly.
15. **Published playable build on itch.io** 

### Grad Requirement: Advanced Feature
The Save/Load system described above (autosave, persistent state across
sessions, pause-and-resume flow) fulfills the graduate-level advanced
feature requirement.


## External Assets / Resources Used
- Guardian character model: https://www.mixamo.com/#/?page=1&type=Character
- Player character model: https://www.mixamo.com/#/?page=3&type=Character
- Music: https://pixabay.com/music/search/horror%20ambience/
- Base player controller: Unity Starter Assets — Third Person Character
  Controller (Unity Technologies)

## Links
- itch.io Playable Build: https://giovatennis.itch.io/ruins-expedition
- YouTube Demo Video: [link]

## Credits
Built by Giovanni Bellio Rincon for Final Project.
