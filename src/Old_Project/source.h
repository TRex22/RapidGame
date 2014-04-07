#ifndef _SOURCE_H_
    #define _SOURCE_H_
    
    #include <time.h>
    
    #ifdef _WIN32
		#define windows TRUE
		#include <windows.h>
	#else
		#define windows FALSE
	#endif
    
    
    void CreateRectangle(SDL_Surface*, SDL_Rect *, int, int, int, int, int);
	void printWorld(char** , int , int , SDL_Surface* , SDL_Rect , int , int );
	void delay();
	Search* DrawPath(Grid* , int , int , SDL_Surface* , SDL_Rect);
	
#endif
