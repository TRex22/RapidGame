using System.Threading;
using Microsoft.Xna.Framework;

namespace RapidXNA.Interfaces
{
    public abstract class IGameScreen
    {
        /// <summary>
        /// Engine instance for the game screen to leverage
        /// </summary>
        public RapidEngine Engine;

        public bool IsLoaded { get; private set; }

        /// <summary>
        /// Lets ScreenService know if this screen has a LoadScreen section
        /// </summary>
        protected bool LoadScreen = false;
        public bool HasLoadScreen { get { return LoadScreen; } }

        public void BeginLoad()
        {
            var ts = new ThreadStart(LoadGameScreenAsync);
            var loadThread = new Thread(ts);
            loadThread.Start();
        }

        private void LoadGameScreenAsync()
        {
            Load();
            IsLoaded = true;
        }

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
