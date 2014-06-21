using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using RapidXNA.Interfaces;
using RapidXNA.Models;
using RapidXNA.Services;

namespace RapidXNA
{
    /*TODO JMC
     * Implement Audio framework
     * Implement input framework
     * Implement Media framework
     * Implement Gamer Services
     * Implement data structures
     * Merge helpers
     * Start screen issue
     * implement error capillary rise ie catches bubble to an error service
     * Resharper Parameters rules evaluate
     * Past TODO's close
     * Perhaps have local configs for WP7 and XBOX to solve problem
     * Do some parallel stuff
     * Cuda implementation as well
     */

    /// <summary>
    /// The Main Part of RapidXNA. This is the engine class.
    /// The Audio framework requires certain input and so it will
    /// be an overloaded constructor. That way there will be backwards
    /// Compatibility for older projects.
    /// </summary>
    public class RapidEngine
    {
        private readonly GraphicsDevice _graphicsDevice;
        /// <summary>
        /// The Graphics Device Object. This is the object responsible for connecting to the internal
        /// XNA framework's GraphicsDevice object in the Rapid framework. 
        /// For more information please refer to <see href="http://msdn.microsoft.com/en-us/library/microsoft.xna.framework.graphics.graphicsdevice.aspx">XNA Documentation</see>
        /// </summary>
        public GraphicsDevice GraphicsDevice { get { return _graphicsDevice; } }
        private readonly ContentManager _contentManagerManager;
        /// <summary>
        /// The Content Manager Object. This is the object responsible for connecting to the internal
        /// XNA framework's ContentManager object in the Rapid framework.
        /// For more information please refer to <see href="http://msdn.microsoft.com/en-us/library/microsoft.xna.framework.content.contentmanager.aspx">XNA Documentation</see>
        /// </summary>
        public ContentManager ContentManager { get { return _contentManagerManager; } }
        private readonly SpriteBatch _spriteBatch;
        /// <summary>
        /// The Sprite Batch Object
        /// For more information please refer to <see href="http://msdn.microsoft.com/en-us/library/microsoft.xna.framework.graphics.spritebatch.aspx">XNA Documentation</see>
        /// </summary>
        public SpriteBatch SpriteBatch { get { return _spriteBatch;  } }

        private readonly EngineServices _engineServices;
        /// <summary>
        /// An object of the Engine Services. This allows a gateway to all the available engine services which are not
        /// directly accessible.
        /// </summary>
        public EngineServices EngineServices { get { return _engineServices; } }
        
        /// <summary>
        /// Quick Access Screen Service. This is one of the Engine Services which is directly accessible.
        /// This service controls the screens created through the RapidXNA framework.
        /// </summary>
        public ScreenService ScreenService { get { return (ScreenService)_engineServices[0]; } }
        /*TODO JMC check the documentation for the input service*/
        /// <summary>
        /// Quick Access Input Service. This is one of the Engine Services which is directly accessible.
        /// The Input Service helps to deal with different types of inputs for different platforms.
        /// </summary>
        public InputService InputService { get { return (InputService)_engineServices[1]; } }

        /// <summary>
        /// Quick access to the UI Service. This is ony of the Engine Services which is directly accessible.
        /// The UI Service gives some basic UI functionality to RapidGame in an easy-to-use manner.
        /// </summary>
        public UIService UIService { get { return (UIService)_engineServices[2]; } }


        /// <summary>
        /// Event handler for Final Draw
        /// </summary>
        public event OnFinalDrawEventHandler OnFinalDraw;

        readonly Game _game;
        readonly RenderTarget2D _renderTarget;
        private readonly Color _defaultClearColour;
        private readonly Color _defaultDrawColour;
        
        /// <summary>
        /// Rapid Engine Default Constructor Without Audio Framework.
        /// The Default colours can be controlled by using the SettingsService.
        /// This method has been depreciated.
        /// To use this method all exit calls must be made in the game1.cs class.
        /// <seealso cref="SettingsManager.LoadSettings"/>
        /// </summary>
        /// <param name="game"></param>
        /// <param name="graphicsDevice"></param>
        /// <param name="contentManagerManager"></param>
        /// <param name="initialGameScreen"></param>
        public RapidEngine(
            Game game, 
            GraphicsDevice graphicsDevice, 
            ContentManager contentManagerManager, 
            GameScreen initialGameScreen)
        {

            /*TODO JMC Find a better fix*/
            _defaultClearColour = Color.Black;
            _defaultDrawColour = Color.White;

            _game = game;
            
            _graphicsDevice = graphicsDevice;
            _contentManagerManager = contentManagerManager;
            
            _spriteBatch = new SpriteBatch(_graphicsDevice);
            
            _renderTarget = new RenderTarget2D(graphicsDevice, graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height);

            _engineServices = new EngineServices(this);
            _engineServices.Add(new ScreenService());
            _engineServices.Add(new InputService());
            _engineServices.Add(new UIService());
            
            ScreenService.Show(initialGameScreen);
        }
        
        /*TODO JMC Check:
         * Check the see also here
         * http://msdn.microsoft.com/en-us/library/bb203874.aspx
         * http://msdn.microsoft.com/en-us/library/microsoft.xna.framework.game.exit.aspx
         * http://stackoverflow.com/questions/12649967/xna-game-isnt-exiting-after-calling-game-exit*/
         
         
        /// <summary>
        /// Allow exiting of the game.
        /// This will also force an immediate garbage collection of all generations.
        /// <seealso cref="EngineServices.Exit"/>
        /// </summary>
        public void Exit()
        {
            _engineServices.Exit(_game);
        }

        

        /// <summary>
        /// Normal XNA Update
        /// For more information please refer to <see href="http://msdn.microsoft.com/en-us/library/microsoft.xna.framework.game.update.aspx">XNA Documentation</see>
        /// </summary>
        public void Update(GameTime gameTime)
        {
            _engineServices.Update(gameTime);
        }

        /// <summary>
        /// Normal XNA Draw
        /// The Default colours can be controlled by using the SettingsService.
        /// <seealso cref="SettingsManager.LoadSettings"/>
        /// For more information please refer to <see href="http://msdn.microsoft.com/en-us/library/microsoft.xna.framework.game.draw.aspx">XNA Documentation</see>
        /// </summary>
        public void Draw(GameTime gameTime)
        {
            _graphicsDevice.SetRenderTarget(_renderTarget);
            _graphicsDevice.Clear(_defaultClearColour);
            _engineServices.Draw(gameTime);
            _graphicsDevice.SetRenderTarget(null);

            if (OnFinalDraw != null)
            {
                //Something has been hooked in to do post processing;
                OnFinalDraw(_renderTarget);
            }

            _spriteBatch.Begin();
            /*TODO JMC allow for different draw colours*/
            _spriteBatch.Draw(_renderTarget, _renderTarget.Bounds, _defaultDrawColour);
            _spriteBatch.End();
        }
    }

    /// <summary>
    /// Event delegate for Final Draw
    /// </summary>
    /// <param name="renderTarget">The renderTarget instance from RapidEngine</param>
    public delegate void OnFinalDrawEventHandler(RenderTarget2D renderTarget);
}
