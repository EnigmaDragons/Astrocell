using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoDragons.Core.Graphics;
using MonoDragons.Core.Inputs;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.Render;
using System;
using Microsoft.Xna.Framework.Input;
using MonoDragons.Core.Development;
using MonoDragons.Core.Entities;
using MonoDragons.Core.KeyboardControls;
using MonoDragons.Core.Motion;
using MonoDragons.Core.MouseControls;
using MonoDragons.Core.Navigation;
using MonoDragons.Core.Render.Animations;
using MonoDragons.Core.Render.Viewports;
using MonoDragons.Core.Text;

namespace MonoDragons.Core.Engine
{
    public class NeedlesslyComplexMainGame : Game
    {
        private readonly string _startingViewName;
        private readonly GraphicsDeviceManager _graphics;
        private readonly SceneFactory _sceneFactory;
        private readonly IController _controller;
        private readonly EntitySystem _ecs;
        private readonly bool _areScreenSettingsPreCalculated;
        
        private SpriteBatch _sprites;
        private Display _display;
        private Size2 _defaultScreenSize;
        private Texture2D _black;

        // @todo #1 fix this so we config everything before the game
        public NeedlesslyComplexMainGame(string title, string startingViewName, Size2 defaultGameSize, SceneFactory sceneFactory, IController controller)
            : this(title, startingViewName, sceneFactory, controller)
        {
            _areScreenSettingsPreCalculated = false;
            _defaultScreenSize = defaultGameSize;
        }

        public NeedlesslyComplexMainGame(string title, string startingViewName, Display screenSettings, SceneFactory sceneFactory, IController controller)
            : this(title, startingViewName, sceneFactory, controller)
        {
            _areScreenSettingsPreCalculated = true;
            _display = screenSettings;
        }

        private NeedlesslyComplexMainGame(string title, string startingViewName, SceneFactory sceneFactory, IController controller)
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            _startingViewName = startingViewName;
            _sceneFactory = sceneFactory;
            _controller = controller;
            MouseSnapshot.MousePositionProvider = new MouseViewport();
            _ecs = Entity.System;
            _ecs.Register(new MotionStateSelector());
            Renderers.RegisterAll(_ecs);
            PhysicsSystems.RegisterAll(_ecs);
            AnimationSystems.RegisterAll(_ecs);
            MouseSystems.RegisterAll(_ecs);
            KeyboardSystems.RegisterAll(_ecs);
            _ecs.Register(new CameraDirector());
            Window.Title = title;
#if DEBUG
            DevelopmentSystems.RegisterAll(_ecs);
#endif
        }

        protected override void Initialize()
        {
            InitDisplayIfNeeded();
            // @todo #1: Update the GraphicsDeviceManager in the constructor, to avoid the window being mispositioned and visibly changing size
            _display.Apply(_graphics);
            Window.Position = new Point(0, 0); // Delete this once the above issue is fixed 
            IsMouseVisible = true;
            _sprites = new SpriteBatch(GraphicsDevice);
            GameInstance.Init(this);
            Input.SetController(_controller);
            _ecs.Register(new ControlHandler());
            _ecs.Register(new DirectionHandler());
            _black = new RectangleTexture(Color.Black).Create();
            Navigate.Init(_sceneFactory);
            DefaultFont.Load(Content);
#if DEBUG
            SceneNavigatorConsole.Enable();
            Metrics.Enable();
            EntityList.Enable(Keys.F12);
#endif
            base.Initialize();
        }

        private void InitDisplayIfNeeded()
        {
            if (!_areScreenSettingsPreCalculated)
            {
                var widthScale = (float)GraphicsDevice.DisplayMode.Width / _defaultScreenSize.Width;
                var heightScale = (float)GraphicsDevice.DisplayMode.Height / _defaultScreenSize.Height;
                var scale = widthScale > heightScale ? heightScale : widthScale;
                _display = new Display((int)Math.Round(_defaultScreenSize.Width * scale), (int)Math.Round(_defaultScreenSize.Height * scale),
                    true, scale);
            }
            CurrentDisplay.Init(_display);
        }

        protected override void LoadContent()
        {
            Navigate.To(_startingViewName);
        }

        protected override void UnloadContent()
        {
            Content.Unload();
        }

        protected override void Update(GameTime gameTime)
        {
            _controller.Update(gameTime.ElapsedGameTime);
            _ecs.Update(gameTime.ElapsedGameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _graphics.GraphicsDevice.Clear(Color.Black);
            _sprites.Begin(SpriteSortMode.FrontToBack, null, SamplerState.AnisotropicClamp, DepthStencilState.DepthRead);
            _ecs.Draw(_sprites, CurrentViewport.Snapshot);
            HideExternals();
            _sprites.End();
            base.Draw(gameTime);
        }

        private void HideExternals()
        {
            _sprites.Draw(_black, new Rectangle(new Point(_display.GameWidth, 0),
                new Point(_display.ProgramWidth - _display.GameWidth, _display.ProgramHeight)), Color.Black);
            _sprites.Draw(_black, new Rectangle(new Point(0, _display.GameHeight),
                new Point(_display.ProgramWidth, _display.ProgramHeight - _display.GameHeight)), Color.Black);
        }
    }
}
