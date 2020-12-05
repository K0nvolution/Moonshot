using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Moonshot.Managers;
using Moonshot.Models;
using Moonshot.Sprites;
using Moonshot.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moonshot.Sprites
{
    public class Player : Actor
    {
        private const int PLAYER_FRAME1_SPRITE_POS_X = 4;
        private const int PLAYER_FRAME2_SPRITE_POS_X = 100 + PLAYER_FRAME1_SPRITE_POS_X;
        private const int PLAYER_FRAME3_SPRITE_POS_X = 100 + PLAYER_FRAME2_SPRITE_POS_X;
        private const int PLAYER_FRAME4_SPRITE_POS_X = 100 + PLAYER_FRAME3_SPRITE_POS_X;
        private const int PLAYER_DEFAULT_SPRITE_POS_Y = 0;
        public const int PLAYER_DEFAULT_SPRITE_WIDTH = 92;
        public const int PLAYER_DEFAULT_SPRITE_HEIGHT = 44;

        private const float FLY_ANIMATION_FRAME_LENGTH = 0.10f; // 10 frames per sec

        #region Fields

        private float _shootTimer = 0;

        public bool IsDead
        {
            get
            {
                return Health <= 0;
            }
        }

        public Input Input;

        // TODO: public PlayerState State

        #endregion

        public Player(Dictionary<string, Animation> animations) : base(animations)
        {
            Speed = 3f;
        }

        /*_position = position;
        Speed = 3f;

        var _playerFrame1Sprite = new Sprite(texture, PLAYER_FRAME1_SPRITE_POS_X, PLAYER_DEFAULT_SPRITE_POS_Y, PLAYER_DEFAULT_SPRITE_WIDTH, PLAYER_DEFAULT_SPRITE_HEIGHT);
        var _playerFrame2Sprite = new Sprite(texture, PLAYER_FRAME2_SPRITE_POS_X, PLAYER_DEFAULT_SPRITE_POS_Y, PLAYER_DEFAULT_SPRITE_WIDTH, PLAYER_DEFAULT_SPRITE_HEIGHT);
        var _playerFrame3Sprite = new Sprite(texture, PLAYER_FRAME3_SPRITE_POS_X, PLAYER_DEFAULT_SPRITE_POS_Y, PLAYER_DEFAULT_SPRITE_WIDTH, PLAYER_DEFAULT_SPRITE_HEIGHT);
        var _playerFrame4Sprite = new Sprite(texture, PLAYER_FRAME4_SPRITE_POS_X, PLAYER_DEFAULT_SPRITE_POS_Y, PLAYER_DEFAULT_SPRITE_WIDTH, PLAYER_DEFAULT_SPRITE_HEIGHT);

        _playerAnimation = new AnimationManager();
        _playerAnimation.AddFrame(_playerFrame1Sprite, 0);
        _playerAnimation.AddFrame(_playerFrame2Sprite, FLY_ANIMATION_FRAME_LENGTH);
        _playerAnimation.AddFrame(_playerFrame3Sprite, FLY_ANIMATION_FRAME_LENGTH * 2);
        _playerAnimation.AddFrame(_playerFrame4Sprite, FLY_ANIMATION_FRAME_LENGTH * 4);
        _playerAnimation.AddFrame(_playerFrame1Sprite, FLY_ANIMATION_FRAME_LENGTH * 5);
        _playerAnimation.Play();
    } */

        public override void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            var velocity = Vector2.Zero;

            if (keyboardState.IsKeyDown(Input.Up))
            {
                velocity.Y -= Speed;
            }
            if (keyboardState.IsKeyDown(Input.Down))
            {
                velocity.Y += Speed;
            }
            if (keyboardState.IsKeyDown(Input.Left))
            {
                velocity.X -= Speed;
            }
            if (keyboardState.IsKeyDown(Input.Right))
            {
                velocity.X += Speed;
            }

            _shootTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (keyboardState.IsKeyDown(Input.Shoot) && _shootTimer > 0.25f)
            {
                Shoot(Speed * 2);
                _shootTimer = 0;
            }

            Position += velocity;

            Position = Vector2.Clamp(_position, Vector2.Zero,
                new Vector2(MoonshotGame.ScreenWidth - _animationManager.FrameHeight, MoonshotGame.ScreenHeight - _animationManager.FrameWidth)); // fix spriteSheet

            _animationManager.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }

        public override void OnCollide(Sprite sprite)
        {
            if (IsDead)
                return;

            //if (sprite is Bullet && sprite.Parent is Enemy)
            //    Health--;

            //if (sprite is Enemy)
            //    Health -= 3;
        }
    }
}
