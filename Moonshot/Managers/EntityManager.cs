using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Moonshot.Sprites;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Linq;
using System.Text;

namespace Moonshot.Managers
{
    public class EntityManager
    {
        private readonly List<GameEntity> _entities = new List<GameEntity>();

        private readonly List<GameEntity> _entitiesToAdd = new List<GameEntity>();
        private readonly List<GameEntity> _entitiesToRemove = new List<GameEntity>();

        public IEnumerable<GameEntity> Entities => new ReadOnlyCollection<GameEntity>(_entities); // readonly wrapper for entities so we don't break encapsulation

        public void Update(GameTime gameTime)
        {
            foreach (GameEntity entity in _entities)
            {
                entity.Update(gameTime);
            }

            foreach (GameEntity entity in _entitiesToAdd)
            {
                _entities.Add(entity);
            }

            foreach (GameEntity entity in _entitiesToRemove)
            {
                _entities.Remove(entity);
            }

            _entitiesToAdd.Clear();
            _entitiesToRemove.Clear();

            PostUpdate(gameTime);
        }

        public void PostUpdate(GameTime gameTime)
        {
            var collidableSprites = _entities.Where(c => c is ICollidable);

            foreach (var spriteA in collidableSprites)
            {
                foreach (var spriteB in collidableSprites)
                {
                    // Don't do anything if they're the same sprite!
                    if (spriteA == spriteB)
                        continue;

                    if (spriteA.CollisionArea.Intersects(spriteB.CollisionArea))
                        ((ICollidable)spriteA).OnCollide((Sprite)spriteB);
                }
            }

            foreach (GameEntity entity in _entities)
            {
                foreach (var child in entity.Children)
                    AddEntity(child);

                entity.Children = new List<Sprite>();
            }
                
        }


        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (GameEntity entity in _entities.OrderBy(e => e.DrawOrder))
            {
                entity.Draw(gameTime, spriteBatch);
            }
        }

        public void AddEntity(GameEntity entity)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity), "Null cannot be added as an entity.");

            _entitiesToAdd.Add(entity); // don't want to influence counter for _entities in Update
        }

        public void RemoveEntity(GameEntity entity)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity), "Null is not a valid entity.");

            _entitiesToRemove.Add(entity); // don't want to influence counter for _entities in Update
        }

        public void Clear()
        {
            _entitiesToRemove.AddRange(_entities);
        }

        public IEnumerable<T> GetEntitiesOfType<T>() where T : GameEntity // generic method that returns all entities of specified type
        {
            return _entities.OfType<T>();
        }
    }
}
