using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace XNAWindowsTestGame
{
    class InitialGameScreen : RapidXNA.Interfaces.IGameScreen
    {
        public override void Load()
        {
            Engine.ScreenService.Pause();
        }

        public override void Update(GameTime gameTime)
        {
            if (Engine.InputService.Keyboard.KeyPress(Keys.Escape))
                Engine.Exit();
        }

        public override void Draw(GameTime gameTime)
        {
            Engine.GraphicsDevice.Clear(Color.Tomato);
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
