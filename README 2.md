# Interaction in Mixed Reality: Basic Concepts

## Assignment 1: Zero Gravity Billiards (Individual Work)

**Deadline: 07.11.2023 15:15**  

### Goals

In this first assignment, you will familiarize yourself with the development environment and practice the basics of Unity development for Mixed Reality, including structuring a scene, using prefabs, applying physics, and (last but not least) implementing interaction with VR controllers. Therefore, you will develop a small billiard game where the billiard playing area is a cube with zero gravity inside.

<img src="Images/zero_gravity_billiards.png"  width="600">

### Tasks

- [ ] [Setup Meta Quest Development Environment and Headset](#setup-meta-quest-development-environment-and-headset) (5 Points)
- [ ] [Setup Unity Project](#setup-unity-project) (5 Points)
- [ ] [Add Unity Project to the Git Repository](#add-unity-project-to-the-git-repository) (5 Points)
- [ ] [Create Scene with Floor and Billiard Playing Area with Pockets](#create-scene-with-floor-and-billiard-playing-area-with-pockets) (5 Points)
- [ ] [Create Cue Ball, Billiard Ball, and Cue Stick as Prefabs and instantiate them on Start](#create-cue-ball-billiard-balls-and-cue-stick-as-prefabs-and-instantiate-them-on-start) (20 Points)
- [ ] [Add Physics to Balls and Stick and provide Zero Gravity](#add-physics-to-balls-and-stick-and-provide-zero-gravity) (5 Points)
- [ ] [Add Ball Bouncing and Decelaration](#add-ball-bouncing-and-decelaration) (5 Points)
- [ ] [Make Pockets attract Balls when touched](#make-pockets-attract-balls-when-touched) (15 Points)
- [ ] [Interaction with cue stick using both controllers](#interaction-with-cue-stick-using-both-controller) (30 Points)
- [ ] [Keep the Project clean](#keep-the-project-clean) (5 Points)

### Instructions

#### Setup Meta Quest Development Environment and Headset

In the Unity Crash Course you learned how to use the Quest with SteamVR. In this course we will use the Oculus Integration SDK. The Oculus Integration SDK allows the application to run on the device without the need for a computer. This means that the application is rendered on the device and not on an external computer. We recommend using the Oculus Integration SDK, but you can still use SteamVR if you prefer.

> ⚠️ If you plan to use SteamVR, make sure it is running on your laptop because you will need to demo your applications in class.

> 💡 If you do not feel comfortable creating a meta account, please contact us. But you don't have to use your personal information for the account. 

- Create a Meta account at [meta.com](https://www.meta.com) (if you do not have one yet)
- Log in with the account on the Quest. Follow the instructions on the device. You will need a smartphone (and the Meta Quest app) to set up the device.
- Turn on the developer mode on your device by using the Meta Quest app (Menu -> Devices -> Meta Quest 2 (or 3) -> Headset Settings -> Developer Settings). The app will tell you that you need a developer account and redirect you.
- Make your account a developer account. You only need to set up two-factor authentication. There is no need to add a credit card.
- Now you should be ready to build on the device.

You find more information and instructions at the official Meta Documentation:
- [Set Up Development Environment and Headset](https://developer.oculus.com/documentation/unity/unity-env-device-setup/)
- [Use Meta Quest Developer Hub and Meta Quest Link](https://developer.oculus.com/documentation/unity/unity-quickstart-mqdh/)

#### Setup Unity Project

See the instructions of the official Meta Documentation:
- [Tutorial - Create Your First VR App on Meta Quest Headset](https://developer.oculus.com/documentation/unity/unity-tutorial-hello-vr/)

#### Add Unity Project to the Git Repository

For this course, basic Git knowledge is required. Therefore, only the specifics of using Git for Unity development will be covered in this exercise. Spoiler: Using Git for collaborative Unity development is not ideal but there is not really a good alternative. But if you don't work on the same scene at the same time, it works quite well. To add the created Unity project to the Git repository follow these steps:

- Clone this Git repository to your computer.
- Copy the contents of your Unity project folder to the Git repository folder.  
  > ⚠️ The Unity project folders like `Assets` should be directly inside the Git repository folder with no folder in between!
- Add a `.gitignore` for Unity development to the Git repository folder. 
  > 💡 We recommend the [Unity gitignore from GitHub](https://github.com/github/gitignore/blob/main/Unity.gitignore) and not the one provided by GitLab as it is outdated.

  > ⚠️ You have to rename the downloaded `Unity.gitignore` to `.gitignore` so that Git recognizes it.
- Add (or stage) all (not ignored) contents of the folder to the Git repository and commit the changes. You can use a commit message like "Add initial Unity project".
- Push the changes.
> ⚠️ From now on you should work directly in the Git repository folder. For this, open the folder of the Git repository in Unity. The previous project folder can be deleted to avoid confusion.

#### Create Scene with Floor and Billiard Playing Area with Pockets

- Create a floor at `y=0` in the color or texture of your choice.
  > 💡 You can use a cube and rescale it
- Create a semi-transparent cube in the color of your choice. To add physics later, use 6 cubes for the sides and rescale them. The cube should have a size of 1x1x1 m and float 1 m above the floor.
  > 💡 It is recommended to structure all objects of the playing area in a parent GameObject, so that it can be repositioned more easily.
- Create semi-transparent spheres as billiard pockets on all 8 corners of the cube. The pockets should have a diameter of 20 cm. Use the color of your choice, but it should be different from the playing area.
  > 💡 We recommend creating the pockets as a prefab. If something is present multiple times in the scene (and has a function), it is recommended to create it as a prefab so that it can be changed more easily.

#### Create Cue Ball, Billiard Balls, and Cue Stick as Prefabs and instantiate them on Start

- Create a cue ball (the white ball in pool billiards) as prefab. The cue ball should have a diameter of 57.15 mm.
- Create a billiard ball as prefab. The billiard ball should have a diameter of 57.15 mm. The billiard ball should have a script with a function that allows to change the ball number (change the material/texture) between 1 and 15.
  > 💡 For the texture of the balls you can use free assets. For example from [Robin Wood](https://www.robinwood.com/Catalog/FreeStuff/Textures/TexturePages/BallMaps.html)
- Create a cue stick as a prefab. The cue stick should be 100 cm long with a diameter of 15 mm. You can use the color or texture of your choice. Place the cue stick at a fixed position of your choice.
  > 💡 You can use a cylinder and rescale it
- Instantiate billiard balls 1 to 14 in a pyramid pattern in the center of the playing area cube. Instantiate billiard ball 15 below the pyramid.
- Instantiate the cue ball inside the playing area cube at a fixed position of your choice. Bonus: You are also allowed to position it at a random position inside the cube, but not overlapping billiard balls.

> ⚠️ The ball objects must not be in the scene before start!

#### Add Physics to Balls and Stick and provide Zero Gravity

- Add a `Rigidbody` to the ball and cue stick prefabs to apply physics.
- Disable `Use Gravity` for the balls to prevent them from being affected by gravity.
- Activate `Is Kinematic` for the cue stick to prevent from being affected by collisions of other objects. However, the cue stick can still affect other objects with a Rigidbody (the balls).
- Change the collision detection of the rigidbodys to support fast moving objects. For the decision have a look at [Continuous collision detection](https://docs.unity3d.com/Manual/ContinuousCollisionDetection.html)

> ⚠️ When manipulating the position of kinematic rigidbodys (e.g., in play mode or with `transform.position`), the physics are not applied correctly because the velocity is not calculated. Therefore, we must use `Rigidbody.MovePosition()` and `Rigidbody.MoveRotation()` for correct physics calculations (we will also use this later for the interaction).

#### Add Ball Bouncing and Decelaration

- Create a `Physic Material` and apply it to the colliders of the balls.
- Set the `Bounciness` value of the material to a value of your choice so that it feels like the bouncing of billiard balls.
  > ⚠️ For bouncing in zero gravity you have to change the `Bounce Threshold` in `Project Seetings` -> `Physics` to `0`.
- For the decelaration of the balls, set the `Drag` value of the balls rigidbodies to a value of your choice so that it feels "realistic". You can also manipulate the `Angular Drag` value to manipulate the decelaration of the rotation.

> ⚠️ `Physic Material` is a simple way to adjust bounciness and friction effects of colliding objects and does not necessarily present a close approximation of real-world physics. See the [Physic Material documentation](https://docs.unity3d.com/Manual/class-PhysicMaterial.html).

#### Make Pockets attract Balls when touched

- Change the colliders of the pockets to triggers (Check `Is Trigger`). The balls can now enter the pockets (but not leave the cube).
- Add a script to the pockets and override the Unity events `OnTriggerEnter()` and `OnTriggerExit()` to check when a ball enters or exits the pocket.
- When a ball enters a pocket, add a force to the rigidbody of the ball so that the ball is attracted to the center of the pocket.
  > 💡 You can use the `AddForce` method of the `Rigidbody` to apply a force.
- So that the ball can leave the cube, use layers and configure the physics between the layers. Add a `Pocket` layer and a `Exit Pocket` layer. Apply the `Pocket` layer to the pockets. When a ball enters a pocket, set the layer of the ball to `Exit Pocket`. Configure the physics between the layers in the `Layer Collision Matrix` in `Project Settings` -> `Physics` so that the balls can leave the cube through the pockets but always can interact with objects in the `Pocket` layer (otherwise the trigger methods of the pocket will not be called).
- When a ball exits a pocket, set `useGravity` of the `Rigidbody` to `true` to apply gravity. Also set the layer back to  `Default` to enable physics between the ball and the floor.

#### Interaction with cue stick using both controller

- Make the Quest controllers visible in the scene. See [Runtime Controllers](https://developer.oculus.com/documentation/unity/unity-runtime-controller/).

> ⚠️ When manipulating the position of kinematic rigidbodys, the physics are not applied correctly because the velocity is not calculated. The objects only dodge a little, but no force is applied. Therefore we need to use `Rigidbody.MovePosition()` and `Rigidbody.MoveRotation()` for correct physics calculations. However, we also want our cue stick to be centered at the handle position. So we need a special structure in the hierarchy, which will be explained below. But you are also allowed to apply physics manually (e.g., with the `AddForce` method).

- In addition to the existing cue stick prefab, create an empty `GameObject` with the name "Cue Stick Parent" in the hierarchy. Add a `Capsule Collider` to the "Cue Stick Parent" with a `Radius` of `0.015` (= 15 mm) and a `Height` of `0.2` (= 20 cm). Also add a kinematic `Rigidbody` to the "Cue Stick Parent" (check the `Is Kinematic` check mark). This will be the trigger of the handle.
  > 💡 For information on why we need a `Rigidbody` for the trigger events, see the table of the [Introduction to Collision](https://docs.unity3d.com/Manual/CollidersOverview.html) (table at the bottom).
- Add a handle visualization to the "Cue Stick Parent" as a child. You can use a `Cylinder` with a scale of `0.016, 0.1, 0.016` to be a little bit thicker than the stick and 20 cm tall. Remove the `Capusle Collider` component because the collider of the handle is already added to the parent object.
  > 💡 When adding the handle visualization to the "Cue Stick Parent" instead of the "Cue Stick Prefab", it will not follow the stick perfectly. You can add it to the "Cue Stick Parent" instead and adjust the position.
- Add a empty `GameObject` to the "Cue Stick Parent" and call it for example "Cue Stick Ghost". We need this ghost object to know the position where the cue stick should be moved. However, we will have to manually move the cue stick to this position (and rotation) with the methods `Rigidbody.MovePosition()` and `Rigidbody.MoveRotation()` for correct physics calculations. Change the local position of the ghost object to `0, 0.4, 0` to set the center of a 100 cm long cue stick.
- Now the structure in the hierarchy should look like this:
  - Cue Stick Prefab
  - Cue Stick Parent
    - Cue Stick Handle Visualization
    - Cue Stick Ghost
  > 💡 With this structure, the center of our cue stick parent is at the handle, and we can still manually set the position of the cue stick's rigid body to get correct physics calculations.
- Create a script for the cue stick and apply it to the "Cue Stick Parent".
- Add a reference to the "Cue Stick Ghost" `GameObject` and the "Cue Stick Prefab" `Rigidbody` to the script (and drop them on the references). Create a `FixedUpdate()` method. Update the position and rotation of the "Cue Stick Prefab" to the position and rotation of the "Cue Stick Ghost" using the methods `Rigidbody.MovePosition()` and `Rigidbody.MoveRotation()` in the `FixedUpdate()` method.
> 💡 From now on we can manipulate the position and rotation of the "Cue Stick Parent" and the "Cue Stick Prefab" will move with it and apply the correct physics. You can try it out in the play mode.
- In the cue stick script, call the methods `OVRInput.Update()` in the `Update()` method and `OVRInput.FixedUpdate()` in the `FixedUpdate()` method to get updates of the controllers states. See also [Map Controllers](https://developer.oculus.com/documentation/unity/unity-ovrinput/)
- See [Map Controllers](https://developer.oculus.com/documentation/unity/unity-ovrinput/) to learn how to access the state of the trigger buttons of both controller.
- Implement the movement of the cue stick with the right controller. When the right controller is inside the handle and you press the trigger, the cue stick should follow the controller (cue stick parent position (= handle position) should follow controller position). When you release the trigger, the cue stick should remain in its current position. 
 > 💡 To check if the right controller is inside the handle, you could add a collider to the `RightControllerAnchor` of the `OVRCameraRig`, check the `Is Trigger` checkmark of the "Cue Stick Parent" collider and override the methods `OnTriggerEnter()` and `OnTriggerExit()` in the cue stick script.
- Implement the aiming of the cue stick with the left controller. When the left controller trigger is pressed (while the right controller trigger is pressed) the cue stick should slide along the left controller (similar to real billiards).
 > 💡 To rotate an object to point at another object you can use the method `transform.LookAt()`. However, this method rotates the forward vector (blue axis) of the object to point at the other object. If you want to use an other axis, you can additionally rotate the object after calling the `LookAt()` method. For example, to rotate the green axis to point to the other object you can use `transform.RotateAround(transform.position, transform.right, 90)` after calling the `LookAt()` method.

#### Keep the Project clean

- Keep the Hierarchy structured. Structure objects that belong together in a parent GameObject. Exception are libraries that require a certain structure.
- Give each GameObject and Prefab a meaningful name. "GameObject (1)" and "Cube" is meaningless. This is especially a problem during debugging when you instantiate objects and don't know which object is what.
- Keep the folder structure clean. For example, you should create folders for Materials, Prefabs, Scripts, and so on. But also a folder structure that groups by feature is fine. There should be no scripts or assets directly in the Assets folder.
