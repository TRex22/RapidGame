using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using RapidXNA_3._0.Interfaces;
using RapidXNA_3._0.Services;

namespace RapidXNA_3._0
{
    /*TODO JMC
        Implement Audio framework
     * Implement input framework
     * Implement Media framework
     * Implement Gamer Services
     * Implement data structures
     * Merge helpers
     * Start screen issue
     * implement error capillary rise ie catches bobblue to an error service
     * Resharper Parameters rules evaluate
     * Past TODO's close
     */
    public class RapidEngine
    {
        readonly Game _game;
        readonly RenderTarget2D _renderTarget;

        public RapidEngine(Game game, GraphicsDevice graphicsDevice, ContentManager contentManager, GameScreen initialGameScreen)
        {
            _game = game;

            _graphicsDevice = graphicsDevice;
            _contentManager = contentManager;
            _spriteBatch = new SpriteBatch(_graphicsDevice);

            _renderTarget = new RenderTarget2D(graphicsDevice, graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height);

            _engineServices = new EngineServices(this);
            _engineServices.Add(new ScreenService());
            _engineServices.Add(new InputService());

            Screen.Show(initialGameScreen);
        }

        /// <summary>
        /// Allow exiting of the game
        /// </summary>
        public void Exit()
        {
            _game.Exit();
        }

        /// <summary>
        /// Instances of the major XNA libraries we will need
        /// </summary>
        private readonly GraphicsDevice _graphicsDevice;
        public GraphicsDevice GraphicsDevice { get { return _graphicsDevice; } }
        private readonly ContentManager _contentManager;
        public ContentManager Content { get { return _contentManager; } }
        private readonly SpriteBatch _spriteBatch;
        public SpriteBatch SpriteBatch { get { return _spriteBatch;  } }
        private readonly EngineServices _engineServices;
        public EngineServices EngineServices { get { return _engineServices; } }
        
        /// <summary>
        /// Helpers for quick access to the standard services
        /// </summary>
        public ScreenService Screen { get { return (ScreenService)_engineServices[0]; } }
        public InputService Input { get { return (InputService)_engineServices[1]; } }

        /// <summary>
        /// Normal XNA Update
        /// </summary>
        public void Update(GameTime gameTime)
        {
            _engineServices.Update(gameTime);
        }

        /// <summary>
        /// Normal XNA Draw
        /// </summary>
        public void Draw(GameTime gameTime)
        {
            _graphicsDevice.SetRenderTarget(_renderTarget);
            //Default clear color: Black
            _graphicsDevice.Clear(Color.Black);

            _engineServices.Draw(gameTime);

            _graphicsDevice.SetRenderTarget(null);

            if (OnFinalDraw != null)
            {
                //Something has been hooked in to do post processing;
                OnFinalDraw(_renderTarget);
            }

            _spriteBatch.Begin();
            _spriteBatch.Draw(_renderTarget, _renderTarget.Bounds, Color.White);
            _spriteBatch.End();
        }

        /// <summary>
        /// Event handler for Final Draw
        /// </summary>
        public event OnFinalDrawEventHandler OnFinalDraw;
    }

    /// <summary>
    /// Event delegate for Final Draw
    /// </summary>
    /// <param name="renderTarget">The renderTarget instance from RapidEngine</param>
    public delegate void OnFinalDrawEventHandler(RenderTarget2D renderTarget);
}
