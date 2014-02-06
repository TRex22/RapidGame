using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace XNAWindowsTestGame
{
    class InitialGameScreen : RapidXNA.Interfaces.IGameScreen
    {
        public override void Load()
        {
            Engine.Screen.Pause();
        }

        public override void Update(GameTime gameTime)
        {
            if (Engine.Input.Keyboard.KeyPress(Keys.Escape))
                Engine.Exit();
        }

        public override void Draw(GameTime gameTime)
        {
            Engine.GraphicsDevice.Clear(Color.Tomato);
        }

        public override void PreLoad()
        {
            Engine.Screen.Init();
        }

        public override void LoadUpdate(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public override void LoadDraw(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public override void OnPop()
        {
            throw new NotImplementedException();
        }
    }
}
