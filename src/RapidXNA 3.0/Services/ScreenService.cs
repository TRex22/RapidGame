using System.Collections.Generic;
using Microsoft.Xna.Framework;
using RapidXNA.Interfaces;

namespace RapidXNA.Services
{
    public class ScreenService : GameService
    {
        /// <summary>
        /// Let RapidEngine know this Service Draws to the screen
        /// </summary>
        public ScreenService()
        {
            DrawEnabled = true;
        }

        /// <summary>
        /// List (faking stack functionality) containing the game screens and popup screens
        /// </summary>
        List<IGameScreen> _gameScreens = new List<IGameScreen>(),
                          _popupScreens = new List<IGameScreen>();

        /// <summary>
        /// Helpers to allow functionality to pause the game
        /// </summary>
        private bool _pause = false;
        public bool Paused { get { return _pause; } }
        public bool Pause()
        {
            _pause = !_pause;
            return _pause;
        }

        public override void Init()
        {
            //No initialisation needed
        }


        /// <summary>
        /// Update either the top popup screen or the top game screen
        /// - Adjusts according to if the screen has a loadscreen or on
        /// </summary>
        public override void Update(GameTime gameTime)
        {
            if (!Paused)
            {
                if (_popupScreens.Count > 0)
                {
                    var popup = _popupScreens[_popupScreens.Count - 1];
                    if (popup.IsLoaded)
                    {
                        popup.Update(gameTime);
                    }
                    else if (popup.HasLoadScreen)
                    {
                        popup.LoadUpdate(gameTime);
                    }
                }
                else if (_gameScreens.Count > 0)
                {
                    var screen = _gameScreens[_gameScreens.Count - 1];
                    if (screen.IsLoaded)
                    {
                        screen.Update(gameTime);
                    }
                    else if (screen.HasLoadScreen)
                    {
                        screen.LoadUpdate(gameTime);
                    }
                }
            }
        }

        /// <summary>
        /// Draws the top game screen, and then if there is a popup screen draws the top popup screen on top of the game screen
        /// - Adjusts depending on whether the screens are loading or not
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            if (_gameScreens.Count > 0)
            {
                var screen = _gameScreens[_gameScreens.Count - 1];
                if (screen.IsLoaded)
                {
                    screen.Draw(gameTime);
                }
                else if (screen.HasLoadScreen)
                {
                    screen.LoadDraw(gameTime);
                }
            }
            //Draw the latest popup screen over the game screen
            if (_popupScreens.Count > 0)
            {
                var popup = _popupScreens[_popupScreens.Count - 1];
                if (popup.IsLoaded)
                {
                    popup.Draw(gameTime);
                }
                else if (popup.HasLoadScreen)
                {
                    popup.LoadDraw(gameTime);
                }
            }
        }

        /// <summary>
        /// Add a new game screen to the top of the stack
        /// - You cannot add a game screen multiple times
        /// </summary>
        /// <param name="gs">The IGameScreen to show.</param>
        public void Show(IGameScreen gs)
        {
            if (_gameScreens.Contains(gs))
                return;
            gs.Engine = Engine;
            gs.PreLoad();
            gs.BeginLoad();

            _gameScreens.Add(gs);
        }

        /// <summary>
        /// Add a new popup screen to the top of the stack
        /// - You cannot add a popup screen multiple times
        /// </summary>
        /// <param name="gs">The IGameScreen to popup.</param>
        public void ShowPopup(IGameScreen gs)
        {
            if (_popupScreens.Contains(gs))
                return;
            gs.Engine = Engine;
            gs.PreLoad();
            gs.BeginLoad();

            _popupScreens.Add(gs);
        }


        /// <summary>
        /// Remove the top game screen
        /// </summary>
        public void Remove()
        {
            if (_gameScreens.Count > 0)
            {
                var gs = _gameScreens[_gameScreens.Count - 1];
                gs.OnPop();
                gs.Engine = null;
                _gameScreens.Remove(gs);
            }
        }

        /// <summary>
        /// Remove the top popup
        /// </summary>
        public void RemovePopup()
        {
            if (_popupScreens.Count > 0)
            {
                var gs = _popupScreens[_popupScreens.Count - 1];
                gs.OnPop();
                gs.Engine = null;
                _gameScreens.Remove(gs);
            }
        }
    }
}
