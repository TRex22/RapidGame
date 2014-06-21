using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using RapidXNA.Models;
using RapidXNA.Models.UI;


namespace XNAWindowsTestGame
{
    class InitialGameScreen : GameScreen
    {
        UIRect ui_item = new UIRect();

        public override void Load()
        {
            Engine.ScreenService.DrawEnabled = true;

            Engine.UIService.Register(ui_item);

            //This is a temporary fix to see where the mouse position origin is - just to prove this does all work correctly
            // - for some reason it is completely off for me
            // - I dont have this problem in MonoGame
            Mouse.SetPosition(0, 0);
        }

        public override void Update(GameTime gameTime)
        {
            if (Engine.InputService.Keyboard.KeyPress(Keys.Escape))
                Engine.Exit();
        }

        public override void Draw(GameTime gameTime)
        {
            Engine.GraphicsDevice.Clear(Color.Black);
        }

        public override void PreLoad()
        {
            Engine.ScreenService.Init();
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
