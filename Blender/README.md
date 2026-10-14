# Blender source files (Lab 3)

- `Hand.blend` – the right-hand model (mesh, UV unwrap, texture, armature, Idle/Grip/Trigger/Fist actions).
- This folder is outside `Assets/` on purpose, so Unity does not try to import the `.blend` file directly.
- The exported model goes to `Assets/Hands/Models/Hand.fbx`; the texture to `Assets/Hands/Textures/Hand_BaseColor.png`.
- `.blend` files are binary and cannot be merged by Git: only one person works in `Hand.blend` at a time.
