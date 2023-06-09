using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MonogameExamples
{

    /// <summary>
    /// Manages a collection of <see cref="System"/> classes and provides methods to add, remove, update and draw entities through them.
    /// </summary>
    public struct SystemManager
    {
        private List<System> _systems = new List<System>();

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemManager"/> class.
        /// </summary>
        /// <param name="LevelID">The ID of the level.</param>
        public SystemManager(LevelID levelID)
        {
            ResetSystems(levelID);
        }

        /// <summary>
        /// Resets the system manager, used whenever a level is changed/reloaded
        /// </summary>
        /// <param name="LevelID">The ID of the level.</param>
        public void ResetSystems(LevelID levelID)
        {
            _systems = new List<System>();

            // Input Systems
            _systems.Add(new PlayerInputSystem());
            _systems.Add(new RegularEnemyInputSystem());

            // Game Logic Systems
            _systems.Add(new MovementSystem());
            _systems.Add(new PlayerEntityCollisionSystem());
            _systems.Add(new ObstacleCollisionSystem(levelID));
            _systems.Add(new RespawnSystem());
            _systems.Add(new AppearSystem());

            // Render Systems
            _systems.Add(new ParallaxSystem());
            _systems.Add(new LevelRenderSystem(levelID));
            _systems.Add(new TimerSystem());
            _systems.Add(new AnimationRenderSystem());

            // Death
            _systems.Add(new DeathSystem());

        }


        /// <summary>
        /// Adds an entity to all the systems.
        /// </summary>
        /// <param name="entity">The entity to be added.</param>
        public void Add(Entity entity)
        {
            foreach (System system in _systems)
            {
                system.AddEntity(entity);
            }
        }

        /// <summary>
        /// Removes an entity from all systems.
        /// </summary>
        /// <param name="entity">The entity to be removed.</param>
        public void Remove(Entity entity)
        {
            foreach (System system in _systems)
            {
                system.RemoveEntity(entity);
            }
        }

        /// <summary>
        /// Subscribes all systems to their MessageBus events.
        /// </summary>
        public void Subscribe()
        {
            foreach (System system in _systems)
            {
                system.Subscribe();
            }
        }

        /// <summary>
        /// Unsubscribes all systems from their MessageBus events.
        /// </summary>
        public void Unsubscribe()
        {
            foreach (System system in _systems)
            {
                system.Unsubscribe();
            }
        }

        /// <summary>
        /// Updates all the systems with the specified <see cref="GameTime"/>.
        /// </summary>
        /// <param name="gameTime">The time since the last update.</param>
        public void Update(GameTime gameTime)
        {
            foreach (System system in _systems)
            {
                system.Update(gameTime);
            }
        }

        /// <summary>
        /// Draws all the entities managed by the systems with the specified <see cref="SpriteBatch"/>.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch used to draw the entities.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (System system in _systems)
            {
                system.Draw(spriteBatch);
            }
        }
    }
}
