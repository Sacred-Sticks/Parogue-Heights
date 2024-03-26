# Parogue Heights

The Endless Ascent is a Unity Game where the player will control a character from an over the shoulder, third person perspective. The player is trying to gain height to avoid the rising threat of acid underneath them. They will utilize a variety of tools and platforms to gain altitude. As they climb, the acid will gain speed in it's own ascent, making it more difficult for the player to keep up.

## Tools and Upgrades

- Trampoline Platforms
  - Players can bounce and jump on trampolines for extra height
- Anti-Gravity Hookshot
  - Pulls the player directly toward the location they shot at
- Swinging Grapple
  - Swings the player from the target they fired towards at, keeping the initial distance they had when the fired the grapple
- Springy Shoes
  - Gives the player extra jump height, allowing them to reach higher locations
- Push Launcher
  - Pushes the player away from where they are currently facing with excessive force, launching them away
- Glue Spray
  - Player can activate a sprayer to make any surface walkable
- Jetpack
  - Player can propel themselves according to their personal upward direction

## System Descriptions
- Special Platforms
  - All platforms will be logged into a PlatformManager's static Dictionary with it's world-space position as a key and the IPlatform implemnentation as the value
- Upgrade Pickups
  - Upgrades will be spawned on a specialty platform and collected when the player lands on it
  - After collection, the platform is removed from the PlatformManager's Dictionary to ensure it is only collected once

___

## Tools and Packages Used

* Created in: Unity 2023.2.14f1
* Using:
  * Kickstarter 2.3.0
  * Cinemachine
  * [SceneReference](git+https://github.com/starikcetin/Eflatun.SceneReference.git#4.0.0)
