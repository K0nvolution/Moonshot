using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Moonshot.Managers;
using Moonshot.Sprites;
using Moonshot.System;
using System.Collections.Generic;

namespace Moonshot
{
    public class MoonshotGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Player _player;
        private InputController _inputController;
        private EntityManager _entityManager;

        public static int ScreenWidth = 1280;
        public static int ScreenHeight = 720;

        public MoonshotGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _entityManager = new EntityManager();
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = ScreenWidth;
            _graphics.PreferredBackBufferHeight = ScreenHeight;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            var playerTexture = Content.Load<Texture2D>("Player_Ship");
            var bulletTexture = Content.Load<Texture2D>("Bullet");
            var sfxShoot_player = Content.Load<SoundEffect>("Laser_Sound");

            var bulletPrefab = new Bullet(bulletTexture)
            {
                Explosion = new Explosion(new Dictionary<string, Models.Animation>()
                {
                    { "Explode", new Models.Animation(Content.Load<Texture2D>("Explosion"), 3) { FrameSpeed = 0.1f, } }
                }),

                Layer = 0.5f,
                ShootSound = sfxShoot_player,
            };

            _player = new Player(new Dictionary<string, Models.Animation>()
            {
                { "In_Place", new Models.Animation(playerTexture, 4) } 
            })
                
            {
                Position = new Vector2(100, 50),
                Layer = 0.3f,
                Bullet = bulletPrefab,
                Input = new Models.Input()
                {
                    Up = Keys.W,
                    Down = Keys.S,
                    Left = Keys.A,
                    Right = Keys.D,
                    Shoot = Keys.Space,
                },
                Health = 10,
            };

            _entityManager.AddEntity(_player);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _entityManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            _entityManager.Draw(gameTime, _spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
