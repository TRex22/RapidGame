using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RapidXNA.Models;
using RapidXNA.Models.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RapidXNA.Services
{
    public class UIService : RapidService
    {
        private Texture2D _blank;
        public override void Init()
        {
            _blank = new Texture2D(Engine.GraphicsDevice, 1, 1);

            Color[] c_data = new Color[1];
            _blank.GetData<Color>(c_data);
            c_data[0] = Color.White;
            _blank.SetData<Color>(c_data);

            //Default to after the other services.
            UpdateOrder = 1;

            //Default to the last thing drawn
            DrawOrder = int.MaxValue;

            DrawEnabled = true;
        }

        private List<UIBlank> _content = new List<UIBlank>();

        /// <summary>
        /// Register a UI item.
        /// </summary>
        /// <param name="ui_item">The item to register</param>
        public void Register(UIBlank ui_item)
        {
            if (!_content.Contains(ui_item))
                _content.Add(ui_item);
        }

        /// <summary>
        /// Deregistter a UI item, this is to be used when a screen is never reused but registered UI elements.
        /// </summary>
        /// <param name="ui_item">The item to deregister.</param>
        public void Deregister(UIBlank ui_item)
        {
            if (_content.Contains(ui_item))
                _content.Remove(ui_item);
        }

        /// <summary>
        /// Deactivate all UI elements, this is to simplify UI workflow.
        /// </summary>
        public void DeactivateAll()
        {
            for (int i = 0; i < _content.Count; i++)
                _content[i].IsActive = false;
        }

        public override void Update(GameTime gameTime)
        {
            var _updateList = (from item in _content
                               where item.IsActive
                               select item).ToList();

            for (int i = 0; i < _updateList.Count; i++)
                _updateList[i].Update(gameTime, this);
        }

        public override void Draw(GameTime gameTime)
        {
            var _drawList = (from item in _content
                             where item.IsActive
                             orderby item.SortOrder
                             select item).ToList();
            if (_drawList.Count > 0)
            {
                Engine.SpriteBatch.Begin();
                for (int i = 0; i < _drawList.Count; i++)
                    _drawList[i].Draw(gameTime, this);
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

        private SpriteFont _defaultFont = null;

        public SpriteFont DefaultFont
        {
            get { return _defaultFont; }
            set { _defaultFont = value; }
        }

        /// <summary>
        /// Draw text using the set default font.
        /// 
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
