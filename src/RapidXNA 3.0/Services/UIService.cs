using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RapidXNA.Models;
using RapidXNA.Models.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RapidXNA.Services
{
    /// <summary>
    /// Quick access to the UI Service. This is the only of the Engine Services which is directly accessible.
    /// The UI Service gives some basic UI functionality to RapidGame in an easy-to-use manner.
    /// </summary>
    public class UIService : RapidService
    {
        private Texture2D _blank;
        /// <summary>
        /// This initiates the UIService.
        /// </summary>
        public override void Init()
        {
            _blank = new Texture2D(Engine.GraphicsDevice, 1, 1);

            var cData = new Color[1];
            _blank.GetData(cData);
            cData[0] = Color.White;
            _blank.SetData(cData);

            //Default to after the other services.
            UpdateOrder = 1;

            //Default to the last thing drawn
            DrawOrder = int.MaxValue;

            DrawEnabled = true;
        }

        private readonly List<UIBlank> _content = new List<UIBlank>();

        /// <summary>
        /// Register a UI item.
        /// </summary>
        /// <param name="uiItem">The item to register</param>
        public void Register(UIBlank uiItem)
        {
            if (!_content.Contains(uiItem))
                _content.Add(uiItem);
        }

        /// <summary>
        /// De-register a UI item, this is to be used when a screen is never reused but registered UI elements.
        /// </summary>
        /// <param name="uiItem">The item to De-register.</param>
        public void Deregister(UIBlank uiItem)
        {
            if (_content.Contains(uiItem))
                _content.Remove(uiItem);
        }

        /// <summary>
        /// Deactivate all UI elements, this is to simplify UI work-flow.
        /// </summary>
        public void DeactivateAll()
        {
            foreach (var t in _content)
                t.IsActive = false;
        }

        /// <summary>
        /// Update the UI created by the UIService
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            var updateList = (from item in _content
                               where item.IsActive
                               select item).ToList();

            foreach (var t in updateList)
                t.Update(gameTime, this);
        }

        /// <summary>
        /// Draw the UI created by the UIService
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            var drawList = (from item in _content
                             where item.IsActive
                             orderby item.SortOrder
                             select item).ToList();
            if (drawList.Count > 0)
            {
                Engine.SpriteBatch.Begin();
                foreach (var t in drawList)
                    t.Draw(gameTime, this);
                Engine.SpriteBatch.End();
            }
        }

        /// <summary>
        /// Draw a blank rectangle on screen of the color specified. Alpha works.
        /// </summary>
        /// <param name="position">The position on screen to draw the rectangle.</param>
        /// <param name="color">The color of the rectangle you want to draw.</param>
        public void DrawBlank(Rectangle position, Color color)
        {
            Engine.SpriteBatch.Draw(_blank, position, color);
        }

        private SpriteFont _defaultFont;

        /// <summary>
        /// This is the constructor method for the default font.
        /// </summary>
        public SpriteFont DefaultFont
        {
            get { return _defaultFont; }
            set { _defaultFont = value; }
        }

        /// <summary>
        /// Draw text using the set default font.
        /// Be sure you set the UIService DefaultFont property before using this method.
        /// </summary>
        /// <param name="text">The text you want to display.</param>
        /// <param name="position">The position you want to display the text on screen.</param>
        /// <param name="color">The color you want to use.</param>
        public void DrawDefaultString(string text, Vector2 position, Color color)
        {
            if (DefaultFont == null)
                throw new Exception("UIService requires you set the DefaultFont property before calling DrawDefaultString(...).");

            Engine.SpriteBatch.DrawString(_defaultFont, text, position, color);
        }

        
    }
}
