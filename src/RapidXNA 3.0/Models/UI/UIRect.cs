using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace RapidXNA.Models.UI
{
    public class UIRect: UIBlank
    {
        private Color _normalColor = new Color(0.8f,0.2f,0.2f,0.5f);

        public Color NormalColor
        {
            get { return _normalColor; }
            set { _normalColor = value; }
        }

        private Color _hoverColor = new Color(0.2f, 0.8f, 0.2f, 0.5f);

        public Color HoverColor
        {
            get { return _hoverColor; }
            set { _hoverColor = value; }
        }

        private bool _hovering = false;

        public bool Hovering
        {
            get { return _hovering; }
            private set { _hovering = value; }
        }

        private Rectangle _position = new Rectangle(5,5,25,25);

        public Rectangle Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gt, Services.UIService _ui)
        {
            if (Hovering)
                _ui.DrawBlank(_position, HoverColor);
            else
                _ui.DrawBlank(_position, NormalColor);
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gt, Services.UIService _ui)
        {
            var mpos = _ui.Engine.InputService.Mouse.Position();
            //Debug.WriteLine(mpos.ToString()); //Used to prove I am not crazy and XNA mouse position is way off.

            if (Position.Contains((int)mpos.X, (int)mpos.Y))
                Hovering = true;
            else
                Hovering = false;
        }
    }
}
