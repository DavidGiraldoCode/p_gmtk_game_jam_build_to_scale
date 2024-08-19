# GMTK Game Jam 2024 - Build to Scale.
A FPS puzzle game where you have to scape rooms using a scaling gun.

## Backlog from earliest to latest.
- [x] Scaling power
- [x] Ease scaling effect
- [x] Change the scalling factor with scroll.
- [x] Apply scaling effect with current scaling factor.
- [x] Left click to stretch and Right click to shrink.
- [x] Add soundFX on shrinking, stretching (use the scale factor to alter pitch)
- [-] Make the scaling a behaviour  
- [x] Create gun mechanics: shoot a ray, visual FX for both types of scaling
- [x] Add gun sound FX
- [~] Add gun cool down before next shoot
- [x] Gun diegetic HUD
- [x] Add particles when shooting the ray
- [x] Review timing between shooting and scaling
- [x] Create a standalone RayBeam GameObject to Instantiate in the scene
- [x] Implement a FS movement: walking and looking
- [x] Assembly FS, gun and scaling systems
- [x] Make the player jump (Character Controller).
- [ ] Start state with UI
- [ ] Reload scene with UI
- [ ] Lose state with UI
- [ ] Winning state with UI
- [ ] Design puzzle one - intro
- [ ] Design puzzle two - explore
- [ ] Design puzzle two - final
- [ ] Shake the camera on every scalling (depends on the cinamachine FP feature)
- [ ] Add gun animations
- [ ] Remove GameObject once they reach a scale threshold
- [ ] Add soundFX on disapearing from shrinking and stretching.
- [ ] Create a partice system where the Geometry disapeared

### Puzle anotations [link](https://www.youtube.com/watch?v=zsjC6fa_YBg)
- Clear objective, the user just needs to figure out how to do it.
- Clear rules and undertandable limitations
- A conflict, an asuption, and a revelation.
- Build on top of the previous knowledge, ramping up in dificulty.
- Steps, solutions, number of mechanics.

## DevLog
- Later on, I realized that the CharacterController was not the best option if I wanted to enable the player to be propelled upward by the scaling geometry.

## References
[1] iHeartGameDev, How to Jump in Unity 3D: Jumping Like Mario [Built-In Character Controller #3], (Jul. 18, 2021). Accessed: Aug. 19, 2024. [Online Video]. Available: https://www.youtube.com/watch?v=h2r3_KjChf4

[2] GDC, Math for Game Programmers: Building a Better Jump, (Dec. 12, 2016). Accessed: Aug. 19, 2024. [Online Video]. Available: https://www.youtube.com/watch?v=hG9SzQxaCm8
