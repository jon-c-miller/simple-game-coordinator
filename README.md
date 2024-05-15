### Simple Game Coordinator

A centralized API for handling requests that deal with common game functionality.

<details>
	<summary>Project Overview</summary>
	<ul><br>
		<b>· Core Direction ·</b><br>
		The goal for this project was to design a game manager solution that was extensible, predictable, simple to access from the codebase, and easy to understand. To accomplish this, GameCoordinator uses the singleton pattern to handle ease of access, allowing any class to execute updates to the game system management classes (sound, particles, etc.). To help offset the fact that any class can access GameCoordinator, the facade pattern is used to shield the game's management classes from direct access by the rest of the codebase, providing only methods such as RequestSound() or RequestParticleEffect() for specific anticipated needs.
		<br><br>
		<b>· Secondary Goals ·</b><br>
		In order to both demonstrate the potential of the GameCoordinator architecture and ensure that the system could grow organically to accommodate common needs, managers for typical game systems were arbitrarily designed and integrated as a part of the project's development. This had the added bonus of providing experience in addressing unforeseen issues in areas that previous projects relied less heavily upon.
		<br><br>
		<b>· Project File Layout ·</b><br>
		The project's <i>Assets</i> folder layout is designed to be approachable while providing practical organization. A hybrid organizational structure is used, neither keeping all assets explicitly organized in separate folders by type, nor keeping assets bundled together by feature or relevance. Instead, code and related prefabs are kept under <i>Elements</i>, while art and other assets are kept reasonably separated by type under <i>Art</i>. This accomplishes the basic objective of separating assets from code while keeping folder count as low as possible for ease of navigation.
    </ul>
</details>

<details>
	<summary>Features</summary>
	<ul><br>
		<b>· Game Coordinator ·</b><br>
		The heart of the project, this element provides a single global access point to filter requests for common game functions that would normally need to be routed separately through independent managers, such as those that handle activating sounds and particle effects, inventory updates, and scene switching. In order to responsibly handle the security risks that a singleton poses, the various managers it keeps references to are kept private, and access to the API is strictly limited to specific method calls. To support scalability, the singleton itself is a partial class, which allows creating separate files to keep methods sorted by relevance, manager type, or whatever seems appropriate to the developer. This design prevents a situation where one file would otherwise grow to hold potentially thousands of methods as the game increases in size and complexity.
		<br><br>
		<b>· Particle Manager ·</b><br>
		This element keeps an IParticleEffect pool for each type of particle effect, and filters effect requests by a ParticleIDs enum. A separate plain class holds references to all effect assets to keep the manager itself from becoming too crowded. Each particle effect prefab has the generic ParticleEffect component attached, which allows its ParticleIDs enum to be specified in the inspector. Support for updating an effect's position, transform, active status, etc. is extended by the IParticleEffect interface inherited by ParticleEffect, which limits and defines what behavior can be expected when effects are handled at runtime. For ease of identification and lookup of interface methods and properties, the convention of prefixing with I is extended to the interface's fields as well (ISetActive, ISetInactive, etc.).
		<br><br>
		<b>· Scene Manager ·</b><br>
		This element handles requests to navigate to the previous/next scene, or to load a specific scene via a SceneIDs enum argument. Scenes are loaded asynchronously to mitigate gameplay effects from engine workload during transitions. A rudimentary progress indicator is also included, which rotates a simple canvas image as long as the load progress is underway. More developed implementations can easily integrate a canvas that fades out the screen between transitions. A DontDestroyOnLoad callback in GameCoordinator ensures that manager references are properly maintained between scenes.
		<br><br>
		<b>· Sound Manager ·</b><br>
		This element handles requests to play a sound, including a sound intended to overlap with multiple concurrent playbacks. Sound requests are filtered by a SoundIDs enum argument. A separate plain class holds references to all sound assets to keep the manager itself from becoming too crowded, and audio sources are added as separate gameObject children. More advanced implementations can make use of queueing to prevent sound issues with the project's simultanous voice count threshold being exceeded (for rapid fire sounds, etc.).
		<br><br>
		<b>· Testing ·</b><br>
		The testing elements in this project act as proxies to demonstrate interaction with GameCoordinator by the rest of the codebase. ParticleActivator simulates activating a one-shot explosion effect at a particular location, activating and deactivating a perpetual sparks effect, and assigning the perpetual sparks effect as a child of an arbitrary object. SceneActivator simulates loading the next scene or previous scene in the project's Scenes In Build, or a specific scene as chosen in the inspector. SoundActivator simulates playing both a single-instance (interruptable) sound, as well as a sound capable of being played multiple times simultaneously (overlapping).
	</ul>
</details>

<details>
	<summary>Reflection On Development</summary>
	<ul><br>
		<b>· Digging Deeper Into Particle Systems ·</b><br>
		Though handling particle effects was secondary to the main focus of the project, there was nevertheless a great opportunity to become more adept at diagnosing and resolving more exotic quirks with Unity's particle system, particularly with regards to reactivation control and how they can be effectively handled with pooling.
		<br><br>
		In one situation, ensuring that the Stop Behavior was set to 'Disable' on the root particle effect and 'None' on any subemitters resolved an issue that was preventing subemitters from being reactivated on subsequent Play() calls to one of the particle effects.
		<br><br>
		In another situation, changing the Culling Mode from 'Pause and Catch-up' to 'Always Simulate' on one of the particle effects' subemitters fixed an issue with that particle effect never having its root's 'Disable' Stop Action called; which prevented the effect from being requeued in the pool as a part of ParticleEffect's OnDisable() callback.
		<br><br>
		<b>· Handling Scene Switching ·</b><br>
		In previous projects, a single scene was sufficient for containing the game world, so this project provided a chance to work with handling scene transitions, make decisions with regards to object persistency, and resolve unforseen obstacles along the way.
		<br><br>
		One minor issue that occurred when using DontDestroyOnLoad with GameCoordinator was that references to its managers would obviously break in a scenario where only the GameCoordinator was kept persistent (and a copy of each manager kept in every scene). It was decided that managers would simply be children of GameCoordinator, thus moving along with it during scene transitions and keeping any references intact. This also had the additional benefit of keeping sounds uninterrupted by scene updates, which makes it possible to maintain player immersion by having persistent background music.
		<br><br>
		However, in a scenario where effects from the ParticleManager were reparented, they would be left behind in the scene they were in. ParticleEffect's OnDisable callback would return the effect's interface to the manager as intended when the level was unloaded, but the actual object would be destroyed on the scene change, causing object access errors when that interface was next dequeued.
		<br><br>
		This was ultimately an indication that more concrete steps needed to be taken to cleanup during scene changing, so logic was added to simply destroy and rebuild the manager's effect pools. This had the added benefit of keeping behavior predictable by ensuring that pools were returned to a base preallocated state rather than having an indeterminate and potentially enormous amount of entries in each pool superfluously carry over to the next scene.
	</ul>
</details>

<details>
	<summary>Ideas for Future Additions</summary>
	<ul><br>
		· Plain 'subclasses' in coordinator/managers to reduce main class sizes
		<br><br>
		· Additional managers:
		<ul>
			- Game state manager
			<br>
			- Camera manager
			<br>
			- Input manager
			<br>
			- Worldspace text manager
			<br>
			- Inventory manager
			<br>
			- Video playback or cutscene manager
			<br>
			- Unit spawning manager (such as for enemies or player-directed instantiating of items)
			<br>
			- Resource manager (such as for currency in an RPG or materials in an RTS)
		</ul>
	</ul>
</details>

<details open>
	<summary>How to Use the Project</summary>
	<ul><br>
		<b>1.</b> Open project in Unity3D as usual
		<br>
		<b>2.</b> Open GameEntry scene and confirm TestActivator keybindings in inspector
		<br>
		<b>3.</b> Play scene
		<br><br>
		<i>Created using Unity version 2022.3.9f1</i>
    </ul>
</details>