using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace MonogameExamples
{
    /// <summary>
    /// <see cref="Component"/> that holds an entity type identifier for classification and management purposes.
    /// Can be replaced by Tag component if game requires it.
    /// </summary>
    public class EntityTypeComponent : Component
    {
        /// <summary>
        /// Gets the type of the entity this component belongs to.
        /// </summary>
        public EntityType Type { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityTypeComponent"/> class with the specified entity type.
        /// </summary>
        /// <param name="type">The type of the entity this component belongs to.</param>
        public EntityTypeComponent(EntityType type)
        {
            Type = type;
        }
    }
}
