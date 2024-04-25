# Parogue Heights

The Endless Ascent is a Unity Game where the player will control a character from an over the shoulder, third person perspective. The player is trying to gain height to avoid the rising threat of acid underneath them. They will utilize a variety of tools and platforms to gain altitude. As they climb, the acid will gain speed in it's own ascent, making it more difficult for the player to keep up.

## Tools and Upgrades

- Bouncepad Platforms
  - Players can bounce and jump on trampolines for extra height
- Anti-Gravity Hookshot
  - Pulls the player directly toward the location they shot at
- JetBoots
  - Player can propel themselves according to their personal upward direction
- Thrusters
  - Launches the player towards the direction they are looking utilizing arm / leg based thrusters
- Smashers
  - Pushes the player away from where they are currently facing with excessive force, launching them away

### System Descriptions
- Special Platforms
  - All platforms will be logged into a PlatformManager's static Dictionary with it's world-space position as a key and the IPlatform implemnentation as the value
- Upgrade Pickups
  - Upgrades will be spawned on a specialty platform and collected when the player lands on it
  - After collection, the platform is removed from the PlatformManager's Dictionary to ensure it is only collected once

## Environments
Instead of using a traditional difficulty escalation, instead it will feature world environments.Each environment will look distinct from the others, giving players a clear sense of progression extemding beyond the score, as they can pay attention to the environment for a rough estimate of height, but still having a final score to determine an exact score.

* Basic Tower
* Fire-Temple
* Earth-Temple
* Ice-Temple
* Air-Temple
* Cosmic-Space

___

## Tools and Packages Used

* Created in: Unity 2023.2.14f1
* Using:
  * Kickstarter 2.3.0
  * Cinemachine
  * [SceneReference](git+https://github.com/starikcetin/Eflatun.SceneReference.git#4.0.0)
