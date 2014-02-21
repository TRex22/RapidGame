using Microsoft.Xna.Framework;

namespace RapidXNA.Interfaces
{
    public interface IGameScreen
    {
        bool IsLoaded { get; }
        bool HasLoadScreen { get; }
        void BeginLoad();
        void LoadGameScreenAsync();

        /// <summary>
        /// Overridable functions of IGameScreen, all are abstract (implying all need to be overidden)
        /// </summary>

        #region OVERRIDABLES

        /// <summary>
        /// Normal Load, Update, Draw methods
        /// Load is threaded
        /// </summary>
        void Load();

        void Update(GameTime gameTime);
        void Draw(GameTime gameTime);

        /// <summary>
        /// Methods for Load, Update, Draw for use when making a loading screen for a given game screen
        /// - This Update and Draw is used while waiting for the asynchronous load to finish
        /// - PreLoad is a synchronous load called before the asynchronous load starts
        /// </summary>
        void PreLoad();

        void LoadUpdate(GameTime gameTime);
        void LoadDraw(GameTime gameTime);
        void OnPop();

        #endregion
    }
}