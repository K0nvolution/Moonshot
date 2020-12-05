using System;
using System.Collections.Generic;
using System.Text;

namespace Moonshot.Sprites
{
    public interface ICollidable
    {
        void OnCollide(Sprite sprite); // sprites can only collide with sprites
    }
}
