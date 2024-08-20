# GMTK Game Jam 2024 - Build to Scale.
An FPS puzzle game where you have to escape rooms using a scaling gun.
<img width="1242" alt="image" src="https://github.com/user-attachments/assets/54e37ad2-8abc-4f57-8ee5-49dd5efab360">


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
- [x] Remove GameObject once they reach a minimum scale threshold
- [x] Change the gun with a bigger one
- [x] Apply texturing to rooms, geometries and hazards
- [x] Import PSX fonts
- [x] Design Start, Lose and Win GUI
- [ ] Add diegetic guides to teach mechanics - Level one: "There an obstacle on the way, shrink it", and so
- [ ] Explain that shapes won't scale more than x2.0
- [ ] The large shapes on levels four and five need to get blocked after scaling it x1.0
- [ ] On level five, the platform needs to be shorter so it won't pass if the player shrinks it
- [ ] Change the color of geometries when reaching max local scale
- [ ] Create the N pre-defined geometries for scaling and update them on the levels
- [ ] Add visual style on GUI
- [ ] Fix mouse sensitivity
- [ ] Shake the camera on every scaling (depends on the cinemachine FP feature)
- [ ] Add a hand holding the gun
- [ ] Reload current level/scene (Pressing R)
- [ ] Add gun animations
- [ ] Add soundFX to disappear from shrinking and stretching.
- [ ] Create a practice system where the Geometry disappeared

### Puzle anotations [link](https://www.youtube.com/watch?v=zsjC6fa_YBg)
- Clear objective. The user just needs to figure out how to do it.
- Clear rules and understandable limitations
- A conflict, an assumption, and a revelation.
- Build on top of the previous knowledge, ramping up in difficulty.
- Steps, solutions, number of mechanics.

## Puzzles
1. A large box is blocking your way, shrink it.
2. A lava river is in your way, but there is a box you can use to stretch it and cross
3. Little lava river, use the boxes on the ceiling so they can fall on the lava
4. Scale a prism to push a ball into a pit and jump on top.
5. Theres a chasm, use the scaling momentum to push a ball to help you cross
5. The exit is high up; use the boxes to create a ramp
6. The way out was hole; use the boxes to create a bridge 

## DevLog
- Later, I realized that the CharacterController was not the best option if I wanted to enable the scaling geometry to propel the player upward.
- The geometries persist on their local scale. A set of elements with pre-defined sizes at local scales of 1.0 is needed so the gun can interact with that.
- Using a cube to manually scale makes it difficult to limit and define the threshold. Having defined geometries of different shapes and sizes allows for homogeneous scaling.
- They may change color to clearly indicate that the geometry has reached its maximum scaling state.

## References
[1] iHeartGameDev, How to Jump in Unity 3D: Jumping Like Mario [Built-In Character Controller #3], (Jul. 18, 2021). Accessed: Aug. 19, 2024. [Online Video]. Available: https://www.youtube.com/watch?v=h2r3_KjChf4

[2] GDC, Math for Game Programmers: Building a Better Jump, (Dec. 12, 2016). Accessed: Aug. 19, 2024. [Online Video]. Available: https://www.youtube.com/watch?v=hG9SzQxaCm8

[3] GDC, Level Design Workshop: Solving Puzzle Design, (Dec. 19, 2017). Accessed: Aug. 19, 2024. [Online Video]. Available: https://www.youtube.com/watch?v=0xBJwrm9C8w
