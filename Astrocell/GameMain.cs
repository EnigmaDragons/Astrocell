using System;
using Astrocell.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Entities;

namespace Astrocell
{
    public sealed class GameMain : Game
    {
        private readonly Func<IScene> _createStartingScene;
        private readonly EntityComponentSystem _ecs;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public GameMain(Func<IScene> createStartingScene)
        {
            _createStartingScene = createStartingScene;
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            _ecs = new EntityComponentSystem(this);
        }

        protected override void Initialize()
        {
            base.Initialize();
            _ecs.Scan(GetType().Assembly);
            _ecs.Initialize();
            _createStartingScene().Setup(_ecs, GraphicsDevice);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Services.AddService(_spriteBatch);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _ecs.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin();
            _ecs.Draw(gameTime);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
