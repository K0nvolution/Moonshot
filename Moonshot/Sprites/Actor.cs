using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Moonshot.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moonshot.Sprites
{
    public class Actor : Sprite, ICollidable
    {
        #region Properties

        public int Health { get; set; }
        public Bullet Bullet { get; set; }

        public float Speed { get; set; }

        #endregion

        public Actor(Dictionary<string, Animation> animations) : base(animations)
        {

        }

        public void Shoot (float speed)
        {
            // We can't shoot if we haven't added a bullet prefab
            if (Bullet == null)
            return;

            var bullet = Bullet.Clone() as Bullet;
            bullet.Position = this.Position;
            bullet.Color = this.Color;
            bullet.Layer = 0.1f;
            bullet.LifeSpan = 5f;
            bullet.Velocity = new Microsoft.Xna.Framework.Vector2(speed, 0f);
            bullet.Parent = this;

            Children.Add(bullet);
        }

        public virtual void OnCollide(Sprite sprite) // player and enemy classes can override OnCollide
        {
            throw new NotImplementedException();
        }

    }
}
