using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moonshot.Sprites
{
    public abstract class GameEntity
    {
        public int DrawOrder { get;  }

        public virtual Rectangle CollisionArea { get; internal set; }

        public virtual List<Sprite> Children { get; set; }

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

    }
}
