#if WINDOWS_PHONE
using System.IO.IsolatedStorage;
using Microsoft.Xna.Framework.Media;
#endif
using System;
using System.IO;
using System.Threading;
using Microsoft.Xna.Framework.Graphics;
using RapidXNA.Models;

namespace RapidXNA.Services
{
    public class ScreenshotService : RapidService
    {
        /// <summary>
        /// Hook onto the RapidEngine FinalDraw event
        /// </summary>
        public ScreenshotService()
        {
            Engine.OnFinalDraw += Engine_OnFinalDraw;
            _renderTarget = new RenderTarget2D(Engine.GraphicsDevice, 1, 1);
        }

        /// <summary>
        /// Gets a copy of the rendertarget
        /// </summary>
        private RenderTarget2D _renderTarget;
        void Engine_OnFinalDraw(RenderTarget2D renderTarget)
        {
            lock (_renderTarget)
            {
                _renderTarget = renderTarget;
            }
        }

        /// <summary>
        /// Take and save a screenshot
        /// </summary>
        public void TakeScreenshot()
        {
            var ts = new ThreadStart(AsyncSaveScreenshot);
            var saveThread = new Thread(ts);
            saveThread.Start();
        }

        /// <summary>
        /// Asynchronously save the screenshot according to which platform you are on
        /// </summary>
        private void AsyncSaveScreenshot()
        {
            lock (_renderTarget)
            {
#if WINDOWS
                try
                {
                    var targetDirectory = Directory.GetCurrentDirectory() + @"\Screenshots\";
                    if (!Directory.Exists(targetDirectory))
                    {
                        Directory.CreateDirectory(targetDirectory);
                    }
                    var tex = (Texture2D)_renderTarget;
                    using (var fs = new FileStream(targetDirectory + "screenshot_" + DateTime.Now.ToString("yyyy_MM_dd_mm_ss") + "_" + DateTime.Now.Millisecond + ".jpg", FileMode.CreateNew))
                    {
                        tex.SaveAsJpeg(fs, tex.Width, tex.Height);
                    }
                }
                catch
                {
                    /*TODO JMC*/
                    throw new NotImplementedException();
                }
#endif
#if WINDOWS_PHONE
                try
                {
                    string tempJPG = "tempJPG";
                    var myStore = IsolatedStorageFile.GetUserStoreForApplication();
                    if (myStore.FileExists(tempJPG))
                    {
                        myStore.DeleteFile(tempJPG);
                    }

                    var fs = myStore.CreateFile(tempJPG);
                    Texture2D tex = (Texture2D)_renderTarget;
                    tex.SaveAsPng(fs, tex.Width, tex.Height);
                    fs.Close();
                    fs = myStore.OpenFile(tempJPG, FileMode.Open, FileAccess.Read);

                    MediaLibrary lib = new MediaLibrary();

                    lib.SavePicture("screenshot_" + DateTime.Now.ToString("yyyy_MM_dd_mm_ss") + "_" + DateTime.Now.Millisecond.ToString() + ".jpg", fs);

                    fs.Close();
                }
                catch
                {

                }
#endif
#if XBOX
                //TODO
#endif
            }
        }

        /*TODO JMC Find a better option, perhaps edit IRapidService*/

        public override void Init()
        {
            throw new NotImplementedException();
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
