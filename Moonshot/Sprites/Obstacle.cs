using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moonshot.Sprites
{
    public class Obstacle : Sprite, ICollidable
    {
        #region Properties

        public int Health { get; set; }

        public float Speed { get; set; }

        #endregion

        public Obstacle(Texture2D texture) : base(texture)
        {

        }

        public void OnCollide(Sprite sprite)
        {
            throw new NotImplementedException();
        }
    }
}
