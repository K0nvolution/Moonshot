using Microsoft.Xna.Framework;
using Moonshot.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moonshot.Sprites
{
    public class Explosion : Sprite
    {
        private float _timer = 0f;

        public Explosion(Dictionary<string, Animation> animations) : base (animations)
        {

        }

        public override void Update(GameTime gameTime)
        {
            _animationManager.Update(gameTime);

            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Removes the sprite once it's finished animating
            if (_timer > _animationManager.CurrentAnimation.FrameCount * _animationManager.CurrentAnimation.FrameSpeed)
                IsRemoved = true;
        }
    }
}
