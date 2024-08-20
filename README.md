# GMTK Game Jam 2024 - Build to Scale.
A FPS puzzle game where you have to scape rooms using a scaling gun.

## Backlog from earliest to latest.
- [x] Scaling power
- [x] Ease scaling effect
- [x] Change the scalling factor with scroll.
- [x] Apply scaling effect with current scaling factor.
- [x] Left click to stretch and Right click to shrink.
- [x] Add soundFX on shrinking, stretching (use the scale factor to alter pitch)
- [x] Make the scaling a behaviour  
- [x] Create gun mechanics: shoot a ray, visual FX for both types of scaling
- [x] Add gun sound FX
- [x] Add gun cool down before next shoot
- [x] Gun diegetic HUD
- [x] Add particles when shooting the ray
- [x] Review timing between shooting and scaling
- [x] Create a standalone RayBeam GameObject to Instantiate in the scene
- [x] Implement a FS movement: walking and looking
- [x] Assembly FS, gun and scaling systems
- [x] Make the player jump (Character Controller).
- [x] Start state with UI
- [x] Lose state with UI
- [x] Winning state with UI
- [x] Reload from scene one
- [x] Design puzzle one - intro
- [x] Design puzzle two - explore
- [x] Design puzzle two - final
- [x] Add a crosshair 
- [x] Remove GameObject once they reach a minimun scale threshold
- [ ] Change gun with bigger one
- [ ] Change color of geometries when reaching max local scale
- [ ] Create the N pre-defined geometries for scaling and updated them on the levels
- [ ] Apply texturing to rooms, geometries and hazards
- [ ] Add diegetic guides to teach mechanics
- [ ] Add visual style on GUI
- [ ] Fix mouse sensitivity
- [ ] Shake the camera on every scalling (depends on the cinamachine FP feature)
- [ ] Add a hand holding the gun
- [ ] Reload current level/scene (Pressing R)
- [ ] Add gun animations
- [ ] Add soundFX on disapearing from shrinking and stretching.
- [ ] Create a partice system where the Geometry disapeared

### Puzle anotations [link](https://www.youtube.com/watch?v=zsjC6fa_YBg)
- Clear objective, the user just needs to figure out how to do it.
- Clear rules and undertandable limitations
- A conflict, an asuption, and a revelation.
- Build on top of the previous knowledge, ramping up in dificulty.
- Steps, solutions, number of mechanics.

## Puzzles
1. A large box is blocking your way, skrink it.
2. A lava river is in your way, but there is a box you can use to stretch it and cross
3. Little lava river, use the boxes on the cealing so they can fall on the lava
4. Scale a prism to push a ball into a pit and jump on top.
5. Theres a shasm, use the scaling momentum to push a ball to help you cross
5. The exit is high up, use the boxes to create a ramp
6. The way out was hole, use the boxes to create a bridge 

## DevLog
- Later on, I realized that the CharacterController was not the best option if I wanted to enable the player to be propelled upward by the scaling geometry.
- The geometries persist on their local scale. There's a need to have a set of elements with pre-defined sizes at local scales of 1.0 so the gun can interact with that.
- Using a cube manually scaling it makes difficult to limit and define the threshold. Having defined geometries of different shapes and sizes allows for homogeneous scaling.
- To create a clear feedbck that the geometry has reached it maximun scaling state, they may change color.

## References
[1] iHeartGameDev, How to Jump in Unity 3D: Jumping Like Mario [Built-In Character Controller #3], (Jul. 18, 2021). Accessed: Aug. 19, 2024. [Online Video]. Available: https://www.youtube.com/watch?v=h2r3_KjChf4

[2] GDC, Math for Game Programmers: Building a Better Jump, (Dec. 12, 2016). Accessed: Aug. 19, 2024. [Online Video]. Available: https://www.youtube.com/watch?v=hG9SzQxaCm8

[3] GDC, Level Design Workshop: Solving Puzzle Design, (Dec. 19, 2017). Accessed: Aug. 19, 2024. [Online Video]. Available: https://www.youtube.com/watch?v=0xBJwrm9C8w