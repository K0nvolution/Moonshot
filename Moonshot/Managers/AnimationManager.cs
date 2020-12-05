using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Moonshot.Models;
using Moonshot.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Moonshot.Managers
{
    public class AnimationManager : ICloneable
    {
        private Animation _animation;

        private float _timer; // to know when to increment frame

        public Animation CurrentAnimation
        {
            get
            {
                return _animation;
            }
        }

        public float Layer { get; set; }

        public Vector2 Origin { get; set; }

        public Vector2 Position { get; set; }

        public float Rotation { get; set; }

        public float Scale { get; set; }

        public int FrameHeight { get { return _animation.FrameHeight; } }

        public int FrameWidth {  get { return _animation.FrameWidth; } }

        public AnimationManager(Animation animation)
        {
            _animation = animation;

            Scale = 1f;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                _animation.Texture,
                Position,
                new Rectangle(
                    _animation.CurrentFrame * FrameWidth, // if CurrentFrame is 0, will look at left side
                    0,
                    FrameWidth,
                    FrameHeight
                ),
                Color.White,
                Rotation,
                Origin,
                Scale,
                SpriteEffects.None,
                Layer
                );
        }

        public void Play(Animation animation)
        {
            if (_animation == animation) // check if we are already playing current animation, don't want to reinstantiate values
                return;

            _animation = animation;

            _animation.CurrentFrame = 0; // set these to 0 to start at start

            _timer = 0;
        }

        public void Stop()
        {
            _timer = 0f;

            _animation.CurrentFrame = 0;
        }

        public void Update(GameTime gameTime)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds; // add to the timer

            if (_timer > _animation.FrameSpeed) // check if done with the current frame
            {
                _timer = 0f;

                _animation.CurrentFrame++;

                if (_animation.CurrentFrame >= _animation.FrameCount) // redo the loop
                    _animation.CurrentFrame = 0;
            }
        }

        public object Clone()
        {
            var animationManager = this.MemberwiseClone() as AnimationManager;

            animationManager._animation = animationManager._animation.Clone() as Animation; // deep clone, rather than shallow method-wise clone

            return animationManager;
        }

    }
}

