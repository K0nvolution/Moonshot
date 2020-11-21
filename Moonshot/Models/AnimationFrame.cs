using Moonshot.Sprites;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moonshot.Models
{
    public class AnimationFrame
    {
        private Sprite _sprite; // can use autoproperty, which does same thing

        public Sprite Sprite
        {
            get => _sprite; // expression buddy, short form return = _sprite;
            set
            {
                if (value == null) // make sure sprite is not null. Make sure no one assigns null
                    throw new ArgumentNullException("value", "The sprite cannot be null.");

                    _sprite = value;
            }
        }

        public float TimeStamp { get; } // autoproperty, compiler adding readonly float _timeStamp for set

        public AnimationFrame(Sprite sprite, float timeStamp)
        {
            Sprite = sprite;
            TimeStamp = timeStamp;
        }
    }
}
