using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDL2;

namespace Rapid_Game_SDL_Experiment_1
{
    /*TODO JMC 
     * Get SDL To work
     * Get rectangles, triangles and other basics shapes to work in rendering
     * Get basic shapes to display on screen
     * Experiment with parallel processing
     * Experiment with performance things like clearing cache
     * Build a small demo
     * Will be a 32bit experiment
     * 
     * Using: SDL2 and SDL2#
     *  SDL2 and SDL2# are released under the zlib license. See LICENSE for details.

        SDL2# currently uses parts of OpenTK, which is released under the MIT license.
        See opentk.LICENSE for details.
     * 
     *  I will use my own fork of SDL2#
     *  The releases are located here: https://github.com/TRex22/SDL2-CS/releases/
     **/
    class Program
    {
        static void Main(string[] args)
        {
            var title = "somethng";
            var width = 0;
            var height = 0;
            ScreenDimensions(width, height);
            //The window we'll be rendering to
            
            //Initialize SDL
            if (SDL2.SDL.SDL_Init(SDL2.SDL.SDL_INIT_EVERYTHING) < 0)
            {
                Console.Out.Write("SDL failed: " + SDL.SDL_GetError());
                SDL2.SDL.SDL_Quit();
            }
            else
            {
                var window = SDL2.SDL.SDL_CreateWindow(title, SDL.SDL_WINDOWPOS_UNDEFINED, SDL.SDL_WINDOWPOS_UNDEFINED, width, height, SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN);
                if (window.Equals(null)) 
                {
                    Console.Out.Write("SDL window failed: " + SDL.SDL_GetError());
                    SDL2.SDL.SDL_Quit(); 
                }
                else
                {
                    //Get window surface
                    var screenSurfacePtr = SDL.SDL_GetWindowSurface(window);
                    var rect = new SDL.SDL_Rect {x = 0, y = 0, w = width, h = height};
                    //Fill the surface white
                    createScreen(rect, screenSurfacePtr);

                    //Update the surface
                    SDL.SDL_UpdateWindowSurface(window);

                    //Wait two seconds
                    SDL.SDL_Delay(2000);
                    SDL .SDL_DestroyWindow(window);
                }
            }
            SDL2.SDL.SDL_Quit();
        }

        public static void ScreenDimensions(int width, int height)
        {
            width = 640;
            height = 480;
        }

        public static void createScreen(SDL.SDL_Rect screenPtr, IntPtr screen)
        {
           //SDL.SDL_FillRect(screen.userdata, ref fuckyou, SDL.SDL_MapRGB(screen.format, 0xFF, 0xFF, 0xFF));
        }

        /* --------------MISC FUNCTIONS--------------------- */
        //these are functions which I have found useful
        int PowerFn (int no, int power)
        {
            int ans;
            switch (power)
	        {
	            case 0:
	                ans = 1;
	                break;
	            case 1:
	                ans = no;
	                break;
	            default:
	                ans = no;
	                int j;
	                for (j = 1; j < power; j++)
	                {
	                    ans = ans*no;
	                }
	                break;
	        }
	        return ans;
        }
    }
}
