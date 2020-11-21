using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Moonshot.Models;
using Moonshot.Sprites;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moonshot.System
{
    public class InputController
    {
        private Player _player;
        private KeyboardState _previousKeyboardState;

        public Input Input;

        public InputController(Player player)
        {
            _player = player;
        }

        public void ProcessControls(GameTime gameTime)
        {


            
        }
    }
}
