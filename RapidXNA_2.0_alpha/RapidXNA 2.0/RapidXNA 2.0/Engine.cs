using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using RapidXNA.Interfaces;
using RapidXNA.Services;

namespace RapidXNA
{
    public class RapidEngine
    {
        Game _game;
        RenderTarget2D _RenderTarget;

        public RapidEngine(Game game1, GraphicsDevice graphicsDevice, ContentManager contentManager, IGameScreen initialGameScreen)
        {
            _game = game1;

            _GraphicsDevice = graphicsDevice;
            _ContentManager = contentManager;
            _SpriteBatch = new SpriteBatch(_GraphicsDevice);

            _RenderTarget = new RenderTarget2D(graphicsDevice, graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height);

            _Services = new EngineServices(this);
            _Services.Add(new ScreenService());
            _Services.Add(new InputService());

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
        private GraphicsDevice _GraphicsDevice;
        public GraphicsDevice GraphicsDevice { get { return _GraphicsDevice; } }
        private ContentManager _ContentManager;
        public ContentManager Content { get { return _ContentManager; } }
        private SpriteBatch _SpriteBatch;
        public SpriteBatch SpriteBatch { get { return _SpriteBatch;  } }
        private EngineServices _Services;
        public EngineServices Services { get { return _Services; } }
        
        /// <summary>
        /// Helpers for quick access to the standard services
        /// </summary>
        public ScreenService Screen { get { return (ScreenService)_Services[0]; } }
        public InputService Input { get { return (InputService)_Services[1]; } }

        /// <summary>
        /// Normal XNA Update
        /// </summary>
        public void Update(GameTime gameTime)
        {
            _Services.Update(gameTime);
        }

        /// <summary>
        /// Normal XNA Draw
        /// </summary>
        public void Draw(GameTime gameTime)
        {
            _GraphicsDevice.SetRenderTarget(_RenderTarget);
            //Default clear color: Black
            _GraphicsDevice.Clear(Color.Black);

            _Services.Draw(gameTime);

            _GraphicsDevice.SetRenderTarget(null);

            if (OnFinalDraw != null)
            {
                //Something has been hooked in to do post processing;
                OnFinalDraw(_RenderTarget);
            }

            _SpriteBatch.Begin();
            _SpriteBatch.Draw(_RenderTarget, _RenderTarget.Bounds, Color.White);
            _SpriteBatch.End();
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
