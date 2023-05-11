using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace EC_Framework
{
    /// <summary>
    /// A factory class for creating entities.
    /// </summary>
    static class EntityFactory
    {
        //Parallax Background
        /// <summary>
        /// Creates a parallax background entity.
        /// </summary>
        /// <param name="texture">The texture to use for the background.</param>
        /// <param name="velocity">The velocity of the background.</param>
        /// <returns>The parallax background entity.</returns>
        public static Entity CreateParallaxBackground(string texture, Vector2 velocity)
        {
            Entity background = new Entity();

            //Define the desired area of parallax
            int viewX = GameConstants.SCREEN_WIDTH;
            int viewY = GameConstants.SCREEN_HEIGHT;

            //Parallax
            background.AddComponent(new ParallaxComponent(texture, velocity, Vector2.Zero, viewX, viewY));
            return background;
        }

        /// <summary>
        /// Creates a player entity.
        /// </summary>
        /// <param name="position">The initial position of the player.</param>
        /// <returns>The player entity.</returns>
        public static Entity CreatePlayer(Vector2 position)
        {
            //Empty Player
            Entity player = new Entity();
            player.AddComponent(new EntityTypeComponent(EntityType.Player));

            // Animations
            AnimatedComponent animation = new AnimatedComponent();
            animation.AddAnimation("player_idle", "idle", 1, 11, 20);
            animation.AddAnimation("player_walking", "walking", 1, 12, 20);
            animation.AddAnimation("player_jump", "jump", 1, 1, 20);
            animation.AddAnimation("player_double_jump", "double_jump", 1, 6, 20);
            animation.AddAnimation("player_fall", "fall", 1, 1, 20);
            animation.AddAnimation("player_slide", "slide", 1, 5, 20);
            animation.AddAnimation("player_death", "death", 1, 7, 20);
            animation.AddAnimation("fruits_death", "appear", 1, 6, 20);
            player.AddComponent(animation);

            // States
            player.AddComponent(new StateComponent());
            player.AddComponent(new PlayerInputComponent());

            // Position and transforms
            player.AddComponent(new MovementComponent(position));

            // Collisions
            player.AddComponent(new CollisionBoxComponent(
                    position: position,
                    width: 32,
                    height: 32,
                    vertTopOffset: 8,
                    vertBottomOffset: 0,
                    horLeftOffset: 4,
                    horRightOffset: 6));

            return player;
        }

        public static Entity CreateFruit(Vector2 position, string fruitName)
        {
            // Create an empty coin entity
            Entity coin = new Entity();
            coin.AddComponent(new EntityTypeComponent(EntityType.Coin));

            // Add animations for the fruit
            AnimatedComponent animation = new AnimatedComponent();
            animation.AddAnimation(fruitName + "_idle", "idle", 1, 17, 20);
            animation.AddAnimation("fruits_death", "death", 1, 6, 20);
            animation.AddAnimation("fruits_death", "appear", 1, 6, 20);
            coin.AddComponent(animation);

            // Add the current state and super state for the fruit
            coin.AddComponent(new StateComponent(State.Idle, SuperState.IsOnGround));

            // Add movement component to set initial position
            coin.AddComponent(new MovementComponent(position));

            // Add a collision box component to handle collisions
            coin.AddComponent(new CollisionBoxComponent(
                    position: position,
                    width: 32,
                    height: 32,
                    vertTopOffset: 7,
                    vertBottomOffset: 11,
                    horLeftOffset: 10,
                    horRightOffset: 10));

            return coin;
        }

        public static Entity CreateRegularEnemy(Vector2 position, float leftRange, float rightRange, State initialDirection)
        {
            // Create an empty enemy entity
            Entity enemy = new Entity();
            enemy.AddComponent(new EntityTypeComponent(EntityType.RegularEnemy));

            // Add animations for the enemy
            AnimatedComponent animation = new AnimatedComponent();
            animation.AddAnimation("voodo_idle", "idle", 1, 11, 20);
            animation.AddAnimation("voodo_walking", "walking", 1, 12, 20);
            animation.AddAnimation("fruits_death", "death", 1, 6, 20);
            animation.AddAnimation("fruits_death", "appear", 1, 6, 20);
            enemy.AddComponent(animation);

            // Add the current state and super state for the enemy
            enemy.AddComponent(new StateComponent(initialDirection));
            enemy.AddComponent(new RegularEnemyComponent(position.X, leftRange, rightRange));

            // Add movement component to set initial position
            enemy.AddComponent(new MovementComponent(position));

            // Add a collision box component to handle collisions
            enemy.AddComponent(new CollisionBoxComponent(
                    position: position,
                    width: 32,
                    height: 32,
                    vertTopOffset: 8,
                    vertBottomOffset: 0,
                    horLeftOffset: 4,
                    horRightOffset: 6));

            return enemy;
        }
    }
}
