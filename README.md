# GameDev_FinalProject

Game Name: Ancient Curse Survivor 

Game Concept:

You are an explorer that found some forgotten ruins reclaimed by the forest. 
The ruins hold an Ancient Chest that is said to have 1000-year curse, and can only be lifted once all ancient symbols are collected.
When night falls, Guardian creatures that once protected the temple wake up and hunt anything that moves near them. 
These guardians only chase you at night, so be patient and survive until daylight comes back.
During the day, you are able to rest and heal any damage caused by them.

Goal:

Collect all 5 Ancient Symbols hidden around the forest and bring them to
the Ancient Chest to open it and win. 

Controls:

Look -> Mouse

Jump -> Space

Sprint -> Left Shift (hold)

Interact (gather / open chest) -> E

Rest (heal, day only) -> F

Restart after win/lose -> Enter

Gameplay Systems:

Day/Night Cycle — the world cycles between a 60-second day and a
60-second night.

Symbol Gathering — 5 Ancient Symbol rocks are placed around the forest.
Walking up to one shows a prompt; pressing E collects it and updates the
on-screen counter live.

Resting — pressing F during the day (only shown as an option when
below full health) locks the player in place, plays a sitting animation,
and heals a small amount per tick until full health is reached. 

Guardian AI — 5 creatures, each intentionally placed near a Symbol rock.
Dormant and harmless during the day. At night, they detect the player
within range, chase, and deal chip damage on contact (20% of max health per
hit). If the player breaks line of sight/range, the guardian walks back to
its original post rather than pursuing indefinitely or freezing in place.

Ancient Chest — the win trigger. Shows live progress toward the 5
Symbol requirement when interacted with.

Health System — a UI health bar tracks player HP.
Guardian hits reduce it; resting restores it; reaching zero ends the game
in a loss state.

Win / Lose / Restart — a win panel appears on opening the chest, a
lose panel appears on death, and pressing Enter (or a Restart button)
reloads the level to try again.

Minimap & Compass — an overhead minimap follows the player, and a
heading-based compass bar helps with orientation while exploring the
ruins.

Day/Night Music — background music crossfades between a day track and
a night track based on the current cycle phase.


Checkpoint 2 Requirement Checklist


Continue developing original 3D game — Done. Built on top of the
original class skeleton (movement, camera, resource-gathering
framework, day/night lighting).

Playable 3D world/environment — Done. Forest terrain with ruin
structures placed throughout, forming the explorable space.

Controllable player + working camera — Done. Third-person character
controller with Cinemachine follow camera; player model was swapped
mid-development with full re-wiring of movement, camera target, and
grounded detection.

5+ interactive/collectible/gameplay objects — Done. 5 Ancient
Symbol rocks + the Ancient Chest (6 total), plus 5 Guardian statues as
hazard objects.

3+ interaction systems — Done. 
(1) Gathering Symbols, 
(2) Resting to heal (day only), 
(3) Opening the Ancient Chest (triggers win).

Progress/resource/score tracking — Done. 
Live Symbol counter, checked against the win requirement by the chest and displayed on interaction.

Clear UI feedback — Done. 
Health bar, live Symbol count, 
contextual prompts ("Press E to gather," "Press F to rest," live chest progress).

3+ challenge systems — Done. 
(1) Guardian AI hazard with chase/attack behavior, 
(2) Night time-pressure (guardians only active during the night), 
(3) Resource-guarding placement — each Guardian is intentionally posted beside a Symbol
rock, so gathering at night is inherently more dangerous than by day.

Clear gameplay goal — Done. 
Collect all 5 Ancient Symbols and deliver them to the chest.

Win/lose/restart flow — Done. 
Win panel on chest completion, lose panel on player death, 
Enter key reloads the scene to restart.

Audio — Done. Day/night background music crossfade. 

Testable start to finish without major errors — Done. 
Full loop confirmed playable: gather, rest, get chased/damaged by guardians,
win via chest or lose via death, restart cleanly.