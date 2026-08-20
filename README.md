# 🎮 Ultimate Unity Script Pack 🚀

<div align="center">

[![Unity Version](https://shields.io)](https://unity.com)
[![License](https://shields.io)](LICENSE)
[![Scripts Count](https://shields.io)]()
[![Status](https://shields.io)]()

*A massive, production-ready collection of 54 modular, clean, and optimized utility scripts for your Unity projects!* 🌟

</div>

---

## 📌 Table of Contents 📑
- [✨ About The Project](#-about-the-project)
- [📦 Features Included](#-features-included)
- [🚪 Highlighted Script: Advanced Doors](#-highlighted-script-advanced-doors)
- [⚙️ How To Use](#️-how-to-use)
- [📋 Quick Setup Guide](#-quick-setup-guide)
- [🤝 Contributing](#-contributing)
- [📜 License](#-license)

---

## ✨ About The Project 🎯
Welcome to your go-to toolkit! This repository packs **54 powerful C# scripts** designed to speed up your game development workflow in Unity</inline>. Whether you are building mechanics for player interaction, door controls, inventory toggles, or sound triggers, these plug-and-play modules save you hundreds of hours of coding. ⏱️

---

## 📦 Features Included 🧩
* **54 Total Scripts** covering various gameplay mechanics 📚
* Fully commented, clean, and easy-to-read code structure 💻
* Optimized performance with minimal overhead ⚡
* Fully customizable via the Unity Inspector</inline> 🎛️

---

## 🚪 Highlighted Script: Advanced Doors 🔑
One of the flagship tools in this pack is `AdvancedDoors.cs`, a robust proximity-based interaction script supporting lock/key mechanics, animations, and sound effects! 🔊

### 📋 How `AdvancedDoors` Works:
1. **Proximity Check (`OnTriggerEnter` / `OnTriggerExit`)**: Detects the player layer/tag (`Reach`) to show UI prompts (`openText`, `closeText`).
2. **State Control (`Update`)**: Automatically checks if lock objects are active in the hierarchy to toggle lock/unlock states.
3. **Key Interaction**: Pressing your interact button with a key equipped plays unlock audio (`unlockedSound`) and triggers coroutines smoothly.
4. **Animations & Audio**: Interfaces natively with your Animator</inline> parameters (`Open`, `Closed`) and plays respective audio clips.

---

## ⚙️ How To Use 🛠️
1. Clone or download this repository to your computer. 📥
2. Copy the desired script(s) directly into your Unity Project's</inline> `Assets` folder. 📁
3. Attach the script to your target GameObject</inline> in the scene. 🎯
4. Assign your references (Animators, AudioSources, GameObjects) in the Unity Inspector</inline>. ✅

---

## 🚀 Quick Setup Guide for Advanced Doors 💡
* Attach `AdvancedDoors` to your door game object. 
* Setup a trigger collider on the door and give your player the tag **`Reach`**. 
* Assign your UI text objects, lock visual indicators, key requirements, and sound sources in the inspector slots. 🎚️

---

## 🤝 Contributing 👥
Contributions, suggestions, and bug reports are always welcome! Feel free to fork this repository and submit a pull request. ⭐

---

## 📜 License 📄
Distributed under the <a href="LICENSE">MIT License</a>. See `LICENSE` for more information. ℹ️

<div align="center">
  <p>Written By VICT0R for the Unity Community!☕ </p>
</div>
