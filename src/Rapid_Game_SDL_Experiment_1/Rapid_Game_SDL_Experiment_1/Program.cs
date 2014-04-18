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
            SDL2.SDL.SDL_Init(SDL2.SDL.SDL_INIT_EVERYTHING);

            SDL2.SDL.SDL_Quit();
        }

        protected void CreateRectangle(SDL.SDL_Surface screen, SDL.SDL_Rect Rect, int x, int y, int h, int w, int colour)
        {
            //Set the dimensions of the rectangle
	        Rect.x = x;
	        Rect.y = y;
	        Rect.h = h;
	        Rect.w = w;
	        //determine the colour required:
		        //0 is for black
		        //1 is for white
	        switch(colour)
	        {
		        //black
		        case 0:
		        {
			        //fill the rectangle
			        SDL.SDL_FillRect(screen,Rect,SDL.SDL_MapRGB(screen.format,0,0,0));
			        break;
		        } 
		        //white
		        case 1:
		        {
			        //fill the rectangle
			        SDL.SDL_FillRect(screen,Rect,SDL.SDL_MapRGB(screen.format,255,255,255));
			        break;
		        }
		        //have other cases programmed for future changes like green and blue
		        //red
		        case 2:
		        {
			        //fill the rectangle
			        SDL.SDL_FillRect(screen,Rect,SDL.SDL_MapRGB(screen.format,255,0,0));
			        break;
		        }	
		        //green
		        case 3:
		        {
			        //fill the rectangle
			        SDL.SDL_FillRect(screen,Rect,SDL.SDL_MapRGB(screen.format,0,255,0));
			        break;
		        }	
		        //blue
		        case 4:
		        {
			        //fill the rectangle
			        //if (rand()%2 == 1)
			        //{
				        SDL.SDL_FillRect(screen,Rect,SDL.SDL_MapRGB(screen.format,0,0,255));
			        //}
			        //else
			        //{
				        //SDL_FillRect(screen,Rect,SDL_MapRGB(screen -> format,0,0,55));	
			        //}				
			        break;
		        }
		
	        }
        }

        protected void Optimise(SDL.SDL_Surface screen)
        {
            SDL.SDL_Surface temp = screen;
            //SDL_BlitSurface(temp, NULL, screen, NULL);
            //SDL_Flip(screen);
            //SDL_FreeSurface(temp);
            //screen = SDL_DisplayFormat(temp);
            //SDL_Flip(temp);
            SDL.SDL_RendererFlip(screen); //SDL_Flip
            //SDL_FreeSurface(screen);
            //SDL_FreeSurface(temp);
        }

        protected void printWorld(char** world, int rows, int cols, SDL.SDL_Surface* screen, SDL.SDL_Rect Rect, int RectH, int RectW)
        {
            int i,j, x, y, h = 1, k = 1;;
            //system("cls");
            //printf("Start:\t %d %d\nGoal:\t %d %d\n", start.r, start.c, goal.r, goal.c );
	        Optimise(screen);
	        SDL.SDL_FreeSurface(screen);
	
            for(i = 0; i < rows; i++){
                for(j = 0; j < cols; j++){
                    //printf("%c", world[i][j]);
                    x = (j+h);
			        y = (i+k);
			
			        //delay();
			        if (world[i][j]== 'x')
                    {
            	        //its alive point
            	        //ie a colour not the background / 0
            	        CreateRectangle(screen, &Rect, x, y, RectH, RectW, 4);
                    }
                    else if (world[i][j]== ' ')
                    {
            	        //assume its dead ie if its a border or a blank space
            	        //or anything unexpected
            	        //must be painted as background 0
            	        CreateRectangle(screen, &Rect, x, y, RectH, RectW, 0);
                    }
                    else if (world[i][j]== 'G')
                    {
            	        CreateRectangle(screen, &Rect, x, y, RectH, RectW, 3);
                    }
                    else if (world[i][j]== 'S')
                    {
            	        CreateRectangle(screen, &Rect, x, y, RectH, RectW, 2);
                    }
                    else if (world[i][j]== '-' || world[i][j]== '|')
                    {
            	        //assume its dead ie if its a border or a blank space
            	        //or anything unexpected
            	        //must be painted as background 0
            	        //CreateRectangle(screen, &Rect, x, y, RectH, RectW, 3);
            	        CreateRectangle(screen, &Rect, x, y, RectH, RectW, 0);
                    }
                    else if (world[i][j]== 'o')
                    {
            	        //its alive point
            	        //ie a colour not the background / 0
            	        CreateRectangle(screen, &Rect, x, y, RectH, RectW, 1);
                    }
                    //CreateRectangle(screen, &Rect, x, y, RectH, RectW, 3);
            
                    //SDL_BlitSurface(temp, NULL, screen, NULL);
                    //SDL_Flip(screen);
			        //SDL_FreeSurface(temp);
			
                    h = j*20;
                    k = i*20;
                }
        
  		
  		        SDL.SDL_Event event;
		        SDL.SDL_PollEvent (&event);
		        /*switch (event.type)
			        {
				        case SDL.SDL_QuitEvent:
					        //QUIT
					        break;
				        case SDL.SDL_KEYDOWN:
			                printf( "Key press detected\n" );
			                exit(0);
			                break;
		
		      	        case SDL.SDL_KEYUP:
			                printf( "Key release detected\n" );
			                exit(0);
			                break;
			    
			            case SDL.SDL_KeyboardEvent:
			    	        printf( "Keypad Enter Detected\n" );
			    	        exit(0);
			    	        break;
					
				        //case 
			        }*/
			
		        //delya
                Optimise(screen);
                //SDL_Flip(screen);
                /*if (SDL_Flip(screen) != 0) {
       		        std::cerr << "Failed to swap the buffers: " << SDL_GetError() << std::endl;
  		        }
                //system("pause");
                printf("\n");*/
            }
        }
        /* --------------MISC FUNCTIONS--------------------- */
        //these are functions I have used in FAC and DDS projects
        //which I have found useful
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
