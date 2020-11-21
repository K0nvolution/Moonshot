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
    public class AnimationManager
    {
        private List<AnimationFrame> _frames = new List<AnimationFrame>();

        public AnimationFrame this[int index]
        {
            get
            {
                return GetFrame(index);
            }
        }

        public AnimationFrame CurrentFrame
        {
            get
            {
                return _frames.Where(f => f.TimeStamp <= PlaybackProgress) // query ones where timestamp <= PlaybackProgress
                    .OrderBy(f => f.TimeStamp) // order by timestamp in ascending order
                    .LastOrDefault(); // return last one in enumeration, last or default in case no element in collection that matches
                                      // would return default for target time, which is null
            }
        }

        public float Duration
        {
            get
            {
                if (!_frames.Any())
                    return 0;

                return _frames.Max(f => f.TimeStamp);
            }
        }

        public bool IsPlaying { get; set; }
        public float PlaybackProgress { get; private set; }

        public bool ShouldLoop { get; set; } = true;

        public void AddFrame(Sprite sprite, float timeStamp)
        {
            AnimationFrame frame = new AnimationFrame(sprite, timeStamp);

            _frames.Add(frame);
        }

        public void Update(GameTime gameTime)
        {
            if (IsPlaying)
            {
                PlaybackProgress += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (PlaybackProgress > Duration)
                {
                    if (ShouldLoop)
                        PlaybackProgress -= Duration;
                    else
                        Stop();
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            AnimationFrame frame = CurrentFrame;

            if (frame != null)
                frame.Sprite.Draw(spriteBatch, position);
        }

        public void Play()
        {
            IsPlaying = true;
        }

        public void Stop()
        {
            IsPlaying = false;
            PlaybackProgress = 0;
        }

        public AnimationFrame GetFrame(int index)
        {
            if (index < 0 || index >= _frames.Count)
                throw new ArgumentOutOfRangeException(nameof(index), "A frame with index " + index + " does not exist in this animation.");

            return _frames[index];
        }

        public void Clear()
        {
            Stop();
            _frames.Clear();
        }
    }
}
