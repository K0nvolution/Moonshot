using Microsoft.Xna.Framework.Graphics;
using Moonshot.Sprites;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moonshot.Models
{
    public class Animation : ICloneable // this class if for each animation
    {
        #region Properties

        public int CurrentFrame { get; set; } // what we are viewing
        public int FrameCount { get; private set; } // how many frames there are in texture
        public int FrameHeight { get { return Texture.Height; } } // how high, can use to scale
        public float FrameSpeed { get; set; } // how quickly to animate through the frames. Lower the number, faster it goes
        public int FrameWidth { get { return Texture.Width / FrameCount; } } // width of frame
        public bool IsLooping { get; set; } // can set to false if you don't want to loop
        public Texture2D Texture { get; private set; }

        #endregion

        public Animation(Texture2D texture, int frameCount)
        {
            Texture = texture;

            FrameCount = frameCount;

            IsLooping = true;

            FrameSpeed = 0.2f;
        }

        public object Clone()
        {// turn clone of member of this class. Have objects use same animation dictionary, but not conflict with each other. Animation class is reference type
            return this.MemberwiseClone();
        }
    }
}
