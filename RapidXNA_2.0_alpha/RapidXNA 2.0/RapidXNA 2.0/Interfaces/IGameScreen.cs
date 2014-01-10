using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.Xna.Framework;

namespace RapidXNA.Interfaces
{
    public abstract class IGameScreen
    {
        /// <summary>
        /// Engine instance for the game screen to leverage
        /// </summary>
        public RapidXNA.RapidEngine Engine;

        /// <summary>
        /// Helpers for asynchronous loading and managing when the threaded load is busy
        /// </summary>
        #region LOADING_HELPERS

        private bool isLoaded = false;
        public bool IsLoaded { get { return isLoaded; } }

        /// <summary>
        /// Lets ScreenService know if this screen has a LoadScreen section
        /// </summary>
        protected bool LoadScreen = false;
        public bool HasLoadScreen { get { return LoadScreen; } }

        public void BeginLoad()
        {
            ThreadStart ts = new ThreadStart(LoadGameScreenAsync);
            Thread loadThread = new Thread(ts);
            loadThread.Start();
        }

        private void LoadGameScreenAsync()
        {
            this.Load();
            isLoaded = true;
        }

        #endregion

        /// <summary>
        /// Overridable functions of IGameScreen, all are abstract (implying all need to be overidden)
        /// </summary>

        #region OVERRIDABLES

        /// <summary>
        /// Normal Load, Update, Draw methods
        /// Load is threaded
        /// </summary>
        public abstract void Load();
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(GameTime gameTime);

        /// <summary>
        /// Methods for Load, Update, Draw for use when making a loading screen for a given game screen
        /// - This Update and Draw is used while waiting for the asynchronous load to finish
        /// - PreLoad is a synchronous load called before the asynchronous load starts
        /// </summary>
        public abstract void PreLoad();
        public abstract void LoadUpdate(GameTime gameTime);
        public abstract void LoadDraw(GameTime gameTime);

        public abstract void OnPop();

        #endregion
    }
}
