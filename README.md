# Interaction in Mixed Reality: Basic Concepts

## Assignment 4: Final Project (Group Work) 

**Deadline Concept Presentation: 19.12.2023 15:15**  
**Deadline Final Presentation and Demo: 06.02.2024 15:15**  
**Deadline Submission Final Project: 13.02.2024 15:15**  

### Goals

In this final assignment you will apply learned skills and combine functionality from previous assignments by implementing a game of your choice. You will design and develop different interaction and navigation techniques in a multiplayer VR game for 2 players. It is up to you to define the topic/genre of your game - it may also be a simulator or educational application for example.

### Tasks

- [ ] [Concept Presentation](#concept-presentation) (10 points)
- [ ] [Implementation: Mandatory Features](#implementation-mandatory-features) (60 points)
- [ ] [Implementation: Bonus Features](#implementation-bonus-features) (2 points each)
- [ ] [Final Presentation](#final-presentation) (20 points)
- [ ] [Final Video](#final-video) (10 points)
- [ ] [Keep the project clean](#keep-the-project-clean)


### Instructions

#### Concept Presentation

Present your game concept in a presentation that includes the idea of your proposed project as well as storyboards of your application, explaining how you will realize all the [mandatory implementation features](#implementation-mandatory-features) . You should stick to the following guidelines:

- Presentation time per group: max. **7** minutes! Each group will be strictly held to the time limit.
- Start by introducing yourselves, and the general idea of your application.
- Include a storyboard of your application.
- Discuss each of the [mandatory implementation features](#implementation-mandatory-features)
- Presentations will be followed by discussions.

> 💡 You may include further details in your presentation (e.g., planned bonus features). Just make sure you stick to the time. 

> ⚠️ Upload your presentation slides to your Gitlab group repository as a .pdf or .pptx. File name should be “Group##_FinalProject_ConceptPresentation.pdf/.pptx”

#### Implementation: Mandatory Features

##### Interaction  (20 points)

Support different interactions by using the controller and hand gestures:  
1. Support the manipulation of objects with the controller (as in previous assignments). The controller should represent a tool (matching the hand posture when holding a controller) that enables the manipulation of objects. 
2. Define a hand gesture for creating (instantiating) new virtual objects. These created objects need to be made available for manipulation by both players (i.e., they should be networked, and their ownership should change depending on which player is trying to interact with them).

> 💡 *Examples:* You may detect the user making a fist and holding this for a certain time (dwell-time) to trigger object instantiation. Or you may add a trigger/confirmation action (e.g., involving the second hand, or objects in the environment), at which point the static hand gesture is analyzed. You may also define multiple static gestures to create different objects (e.g., flat hand makes a flat object, fist makes a round object).


##### Navigation (20 points)

Support two different navigation techniques:
1. Navigation within the tracking area: Allow *natural walking* within each user’s tracking area. To enable interaction between users, you will probably need to make sure that their tracking areas overlap/coincide, so that they can reach each other. 
2. Navigation beyond the tracking area: Design a *“distant travel technique”* that allows both users to jointly navigate to another area of the virtual world, which lies beyond their current tracking area (e.g., mount a vehicle to travel somewhere further away and dismount again to walk around). Once they have arrived at the new location, they should again be able to move as described in the point above (1). 

> ⚠️ **Important:** The “distant travel technique” must
> -	be *collaborative*, i.e., require actions by both users (does not necessarily need to be simultaneous/synchronous).
> -	require a *dynamic gesture/physical action* by both users (e.g., flapping their arms – the gesture can be different for each user).
> -	make both users *travel together*.

> 💡 *Examples:* Each person gets an oar and has to row on their side of a boat, one person throws an “anchor” and the other person pulls on it to move their raft to a new destination.

##### Networking (20 points)

Support the synchronization of objects and user representations between clients:
1. Synchronize *all interactable objects* and their changes between clients.
2. *Instantiated objects* should also be synchronized between clients.
3. Create *user representations* that reflect the player's capabilities and synchronize their changes between clients.

##### Visual feedback and overall application experience 

You should provide adequate visual feedback (to both users) for 
1. all interactions (e.g., object manipulations).
2. user movement (particularly activation of the distant travel technique).
3. You should enhance the overall application quality experience from the quality of visualization, animations and user experience.

#### Implementation: Bonus Features

- Physics-based object interaction: Implement throwing of objects (network-synchronized physics). 
- Particle effects: Use particle effects in your game in a way that makes sense. 
- Avatar customization: Enable users to change the appearance of their own or the other player’s avatar (e.g., color). 
- Haptic and auditive feedback: Implement haptic feedback or auditive feedback for interactions in your application.

#### Final Presentation 

The course concludes with a live presentation AND a live demo of your prototype. For the presentation, you should stick to the following guidelines:

- Presentation time per group: max. **5** minutes! Each group will be strictly held to the time limit.
- 2-3 minutes of questions and clarifications will follow the presentation
- Presentation should include following:
    - Introduction of yourselves
    - Explaining the application storyline with showing how application flow (use videos)
    - Showing the storyline for each player (in case of multiplayer games)
    - Explaining how [mandatory implementation features](#implementation-mandatory-features) are met in your application.
    - Mention the bonus features you implemented and how your implementation satisfies this feature/s.
    - Describe one limitation that the application has or a challenge you faced during the assignment.
    - Include videos, gifs and pictures of your application in each of the mentioned points if possible.

> ⚠️ Upload your presentation slides to your Gitlab group repository as a .pdf or .pptx. File name should be “Group##_FinalProject_FinalPresentation.pdf/.pptx”

For the demo, you should stick to the following guidelines:

- Demos will take place after all presentations are held
- You should prepare your demo and take in consideration the following points:
    - Demo is working
    - Casting the display of **one** headset to your laptop through a cable using [Scrcpy](https://github.com/Genymobile/scrcpy) or a comparable solution not using network connection
    - Demo pitch to explain the flow of your application and main controls/gestures for each player.
    - Make sure the demo of the mandatory features doesn't take more than 5 minutes
    - You might be asked during the demo about your application and implementation.
    - It’s recommended to build shortcuts in your application to skip a step if the player is stuck.

#### Final Video

- Create a short video (2 minutes) demonstrating your gameplay and the integration of the features
- Make sure to include your project title and group number in the title screen
- The video should include the gameplay/application from the perspective of both players
- The mandatory features and bonus features should be visible in the video.

> ⚠️ Upload your video to your Gitlab group repository as a .mp4. File name should be “Group##_FinalProject_Video.mp4”. Make sure to compress your file (recommendation below 200 MB).

#### Keep your project clean

- Keep the Hierarchy structured. Objects that belong together should be grouped under a parent GameObject. (Exception: libraries that require a certain structure.)
- Give each GameObject and Prefab a meaningful name. (Avoid something like "GameObject (1)" and "Cube")gless. This can especially cause problems during debugging, when you instantiate objects and don't know which object is what.
- Keep the folder structure clean. For example, you should create folders for Materials, Prefabs, Scripts, and so on. But also a folder structure that groups by feature is fine. There should be no scripts or assets directly in the Assets folder.
