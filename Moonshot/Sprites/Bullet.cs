using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moonshot.Sprites
{
    public class Bullet : Sprite, ICollidable
    {
        #region Fields

        private float _timer;

        public Explosion Explosion;

        public SoundEffect ShootSound;

        #endregion

        #region Properties

        public float LifeSpan { get; set; }

        public Vector2 Velocity { get; set; }

        #endregion

        public Bullet(Texture2D texture)
            : base(texture)
        {

        }

        public override void Update(GameTime gameTime)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_timer >= LifeSpan)
                IsRemoved = true;

            Position += Velocity;
        }

        public void OnCollide(Sprite sprite)
        {
            // Bullets don't collide with each other
            if (sprite is Bullet)
                return;

            // Enemies can't shoot each other
            /*if (sprite is Enemy && this.Parent is Enemy)
                return;

            if ((sprite is Enemy && this.Parent is Player) || (sprite is Player && this.Parent is Enemy))
            {
                IsRemoved = true;
                AddExplosion();
            } */
        }

        public void AddExplosion()
        {
            if (Explosion == null)
                return;

            var explosion = Explosion.Clone() as Explosion;
            explosion.Position = this.Position;

            Children.Add(explosion);
        }

    }
}
