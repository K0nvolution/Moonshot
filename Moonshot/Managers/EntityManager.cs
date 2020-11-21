﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Moonshot.Sprites;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Moonshot.Managers
{
    public class EntityManager
    {
        private readonly List<IGameEntity> _entities = new List<IGameEntity>();

        private readonly List<IGameEntity> _entitiesToAdd = new List<IGameEntity>();
        private readonly List<IGameEntity> _entitiesToRemove = new List<IGameEntity>();

        public IEnumerable<IGameEntity> Entities => new ReadOnlyCollection<IGameEntity>(_entities); // readonly wrapper for entities so we don't break encapsulation

        public void Update(GameTime gameTime)
        {
            foreach (IGameEntity entity in _entities)
            {
                entity.Update(gameTime);
            }

            foreach (IGameEntity entity in _entitiesToAdd)
            {
                _entities.Add(entity);
            }

            foreach (IGameEntity entity in _entitiesToRemove)
            {
                _entities.Remove(entity);
            }

            _entitiesToAdd.Clear();
            _entitiesToRemove.Clear();
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (IGameEntity entity in _entities.OrderBy(e => e.DrawOrder))
            {
                entity.Draw(spriteBatch, gameTime);
            }
        }

        public void AddEntity(IGameEntity entity)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity), "Null cannot be added as an entity.");

            _entitiesToAdd.Add(entity); // don't want to influence counter for _entities in Update
        }

        public void RemoveEntity(IGameEntity entity)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity), "Null is not a valid entity.");

            _entitiesToRemove.Add(entity); // don't want to influence counter for _entities in Update
        }

        public void Clear()
        {
            _entitiesToRemove.AddRange(_entities);
        }

        public IEnumerable<T> GetEntitiesOfType<T>() where T : IGameEntity // generic method that returns all entities of specified type
        {
            return _entities.OfType<T>();
        }
    }
}
