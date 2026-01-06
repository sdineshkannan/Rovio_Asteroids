How to Run:

Unity Version Used : 6000.3.2f1

* Open the project in Standalone, iOS or Android platform
* Open Assets/Scenes/MainGamePlay scene
* Set the Display to any landscape mode
* Hit Run


Movements Keys (For simplicity it's using old input system and designed only for Keyboard. It can be easily converted to mobile touch inputs)

Shoot - Space
Rotate - Left/Right arrows or A/D
Move Forward - Up Arrow or W 

The gameplay values can be adjusted to improve playability. It can be found under Assets/Configs

==================================

Design & Implementation Notes

Modular, single-responsibility design
Gameplay is split into focused components (player movement, shooting, health, waves, scoring) to make the code easy to read, test and extend.

Explicit player state management
Player behavior is controlled via a PlayerState model (Disabled, Active, Dead) rather than booleans. This makes state transitions clear and allows easy extension without refactoring core systems.

Data-driven tuning with ScriptableObjects
Player, wave progression, and asteroid behavior are configured via ScriptableObjects. This allows balancing and future changes (new asteroid types, difficulty tweaks) without modifying code.

Config-driven asteroid types
Asteroids use a shared prefab with sprite/scale driven by configuration to keep the maintenance low. The design supports optional prefab overrides for future asteroid types with unique structure or behavior.

Factory + object pooling for performance
Object creation is encapsulated behind factories. Bullets, explosions and asteroids are pooled to avoid frequent Instantiate/Destroy calls, reducing GC allocations and frame spikes—especially during asteroid splitting.

Event-driven UI and FX
Gameplay systems publish events (score, lives, wave, explosions). UI and FX subscribe to these events to eliminate polling and tightly coupled references.

Physics collision filtering
A dedicated Physics2D collision matrix ensures only meaningful interactions occur (Player↔Asteroid, Bullet↔Asteroid), reducing unnecessary physics checks.


==================================

Challenges faced & decisions made

Avoiding tight coupling between gameplay and UI/FX
Challenge: Direct references and FindObject… calls make systems hard to test and change.
Decision: Use a lightweight event hub (GameEvents) so UI/FX react to state changes without gameplay depending on UI objects.

Performance spikes from Instantiate/Destroy
Challenge: Splitting asteroids and firing bullets cause frequent allocations and frame spikes.
Decision: Introduce pooling behind factories; gameplay code stays clean while runtime churn drops dramatically.

Prefab duplication vs maintainability
Challenge: Separate prefabs for Large/Medium/Small differed only by sprite/scale, which risks inconsistent settings over time.
Decision: Use a single shared prefab and drive sprite/scale via AsteroidTypeConfig, with an optional prefabOverride for future asteroid types that require different structure.

Future extensibility (new asteroid types)
Challenge: Hardcoded switch logic makes new types expensive and risky.
Decision: Make behavior and tuning config-driven (ScriptableObjects) and keep creation abstracted (factories), so adding new types is mostly “add config + optionally prefab”.