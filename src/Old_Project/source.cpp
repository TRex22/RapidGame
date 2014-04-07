#include <stdio.h>
#include <stdlib.h>

#include <iostream>
#include <cstdio>

#include <time.h>
#include <unistd.h>
#include "Grid.h"

#include "SDL/SDL.h"

#include "LinkList.h"
#include "Stack.h"
#include "Queue.h"

#ifdef _WIN32
		#define windows TRUE
		#include <windows.h>
	#else
		#define windows FALSE
#endif

using namespace std;

void CreateRectangle(SDL_Surface*, SDL_Rect *, int, int, int, int, int);
void printWorld(char** , int , int , SDL_Surface* , SDL_Rect , int , int );
Search* DrawPath(Grid* , int , int , SDL_Surface* , SDL_Rect);
void delay();
void Optimise(SDL_Surface*);


/* ------------- QUEUE FUNCTIONS ------------------ */
Queue::Queue(){
	//Queue *q=(Queue*)malloc(sizeof(Queue));
	//q->l=new LinkList();
	//l=new LinkList();
	//size =0;
	//return q;
	//printf("hello");
	l = new LinkList();
	//l->mysize=0;
}
Queue::~Queue(){
    if(l != NULL)
        delete l;
}

void Queue::enqueue(int x, int y){
	l->addBack(x, y);
	//size++;
}

void Queue::dequeue(int &x, int &y){
	if (l->head== NULL)
	{
		//return;
		//kill bot
	}
	else
	{
		l->getFront(x, y);
	}
	
}

int Queue::size(){
	return l->size();
}

/* ------------- STACK FUNCTIONS ------------------ */
Stack::Stack(){
	//size = 0;
	//printf("hello");
	//Stack *s = (Stack*)malloc(sizeof(Stack));
	//s->l = new LinkList();
	
	//mysize auto set to 0
	l = new LinkList();
	
	//l->mysize=0;
	//l = new LinkList();
	//s->l->mysize = 0;
	//mysize = 0;
	//s->l->mysize=0;
	//printf("size %d\n", size());
	//return s;
}

Stack::~Stack(){
    if(l != NULL)
        delete l;
}

void Stack::push(int x, int y){
	l->addFront(x, y);
}

void Stack::pop(int &x, int &y){
	if (l->head== NULL)
	{
		//return;
		//kill bot
	}
	else
	{
		l->getFront(x, y);
	}
}

int Stack::size(){
	//printf("booby");
	return l->size();
}

/* ------------- SEARCH FUNCTIONS ------------------ */
Grid* ReadWorld(const char* filename){
	FILE *filey = fopen(filename, "r+");
	if (filey == NULL)
	{
		//printf("Error Opening File\n");	
		//just ignore for now not really important for this
	}	
	
	int i,j; //counters
	Grid *theGrid = new Grid;
	//printf("Vars\n");
	fscanf(filey, "%d %d\n", &theGrid->rows, &theGrid->cols);
	//printf("Read file\n");
	theGrid -> data = new char*[theGrid->rows];
	//printf("The Grid malloc\n");
	//printf("Size of The Grid: %d\n", (sizeof(theGrid)));
	//j = theGrid->rows;
	//printf("J: %d\n", j);
	
	for(i=0;i<theGrid->rows;i++)
	{
		theGrid->data[i] = new char[theGrid->cols+3];
		//printf("%s",theGrid->data[i]);
		//printf("Data at i: %s Loop i: %d \n",theGrid->data[i], i);
	}
	
	for (i=0;i<theGrid->rows;i++)
	{
		fgets(theGrid->data[i], theGrid->cols+3, filey);
	}

	fclose(filey);
	//printf("End.\n");
	return theGrid;
}

Search* FindPath(Grid* g){
	Queue *q = new Queue();
	//printf("1\n");
	Search* se = new Search;
	//printf("2\n");
	se->s=new Stack();
	//printf("3\n");
	se->Length=0;
	//printf("4\n");
	int i=0, j=0, rows = g->rows, cols = g->cols;
	
	se->ParentR = new int*[rows];
	se->ParentC = new int*[rows];
	se->Distance= new int*[rows];
	//printf("5\n");
	
	//printf("Here\n");
	//printf("Rows %d\n", rows);
	//printf("i: %d",i);
	
	//system.end(0);
	
	i=0, j=0;
	int startR, startC, goalR, goalC;
	for(i=0;i<rows;i++)
	{
		se->ParentR[i] = new int[cols];
		se->ParentC[i] = new int[cols];
		se->Distance[i]= new int[cols];

		for(j=0;j<cols;j++)
		{	
			se->ParentR[i][j] = -2;
			se->ParentC[i][j] = -2;
			
			se->Distance[i][j] = MAX_INT;
			
			if(g->data[i][j] == 'S')
			{
				startR=i;
				startC=j;
			}else if(g->data[i][j] == 'G'){
				goalR = i;
				goalC = j;
				g->data[i][j] = ' ';
			}

		}
	}
	
	se->Distance[startR][startC]=0;
	se->ParentR[startR][startC]=-1;
	se->ParentC[startR][startC]=-1;

	q->enqueue(startR, startC);
	int currR, currC;
			//printf("hello\n");
	currR = startR;
	currC = startC;
	//printf("Rows: %d Cols: %d\n",rows,cols);
	//printf("queue size: %d\n",q->size());
			//printf("hello\n");
	while((q->size())!=0 && !(currR == goalR && currC == goalC))
	{

		//printf("Rows: %d Cols: %d\n",rows,cols);
		q->dequeue(currR, currC);
		
		//up
		if(g->data[currR-1][currC]==' ' && se->ParentR[currR-1][currC]==-2 && se->ParentC[currR-1][currC]==-2)
		{
			se->Distance[currR-1][currC] = se->Distance[currR][currC] + 1;
			se->ParentR[currR-1][currC] = currR;
			se->ParentC[currR-1][currC] = currC;
			q->enqueue(currR-1, currC);
		}
		
		//down
		if(g->data[currR+1][currC]==' ' && se->ParentR[currR+1][currC]==-2 && se->ParentC[currR+1][currC]==-2)
		{
			se->Distance[currR+1][currC] = se->Distance[currR][currC] + 1;
			se->ParentR[currR+1][currC] = currR;
			se->ParentC[currR+1][currC] = currC;
			q->enqueue(currR+1, currC);
		}
		
		//left
		if(g->data[currR][currC-1]==' ' && se->ParentR[currR][currC-1]==-2 && se->ParentC[currR][currC-1]==-2)
		{
			se->Distance[currR][currC-1] = se->Distance[currR][currC] + 1;
			se->ParentR[currR][currC-1] = currR;
			se->ParentC[currR][currC-1] = currC;
			q->enqueue(currR, currC-1);
		}
		
		//right
		if(g->data[currR][currC+1]==' ' && se->ParentR[currR][currC+1]==-2 && se->ParentC[currR][currC+1]==-2)
		{
			se->Distance[currR][currC+1] = se->Distance[currR][currC] + 1;
			se->ParentR[currR][currC+1] = currR;
			se->ParentC[currR][currC+1] = currC;
			q->enqueue(currR, currC+1);
		}
	}
	
	if(q->size()==0 && g->data[currR][currC]!='G')
	{
		se->Length=-1;
		return se;
	}
	else
	{
		se->Length=se->Distance[currR][currC];
		se->s->push(currR, currC);
		//se->s->
		while(currR != -1 && currC != -1)
		{
			se->s->push(se->ParentR[currR][currC], se->ParentC[currR][currC]);
			int tempR = currR, tempC=currC;
			currR=se->ParentR[tempR][tempC];
			currC=se->ParentC[tempR][tempC];	
		}		
		se->s->pop(currR, currC);
		return se;
	}
}


/* --------------GRAPHICS FUNCTIONS--------------------- */
Search* DrawPath(Grid* g, int RectH, int RectW, SDL_Surface* screen, SDL_Rect Rect){
	Queue *q = new Queue();
	//printf("1\n");
	Search* se = new Search;
	//printf("2\n");
	se->s=new Stack();
	//printf("3\n");
	se->Length=0;
	//printf("4\n");
	int i=0, j=0, rows = g->rows, cols = g->cols;
	
	se->ParentR = new int*[rows];
	se->ParentC = new int*[rows];
	se->Distance= new int*[rows];
	//printf("5\n");
	
	//printf("Here\n");
	//printf("Rows %d\n", rows);
	//printf("i: %d",i);
	
	//system.end(0);
	
	i=0, j=0;
	int startR, startC, goalR, goalC;
	for(i=0;i<rows;i++)
	{
		se->ParentR[i] = new int[cols];
		se->ParentC[i] = new int[cols];
		se->Distance[i]= new int[cols];

		for(j=0;j<cols;j++)
		{	
			se->ParentR[i][j] = -2;
			se->ParentC[i][j] = -2;
			
			se->Distance[i][j] = MAX_INT;
			
			if(g->data[i][j] == 'S')
			{
				startR=i;
				startC=j;
			}else if(g->data[i][j] == 'G'){
				goalR = i;
				goalC = j;
				//g->data[i][j] = ' ';
			}

		}
	}
	//g->data[goalR][goalC] = 'G';
	printf("Press any key to continue...\n");
	getchar();
	SDL_FillRect(screen, NULL, 0);
	printf("\nStart State:\n\n");
	//getchar();
	printWorld(g->data, rows, cols, screen, Rect, RectH, RectW);
	//system("pause");
	printf("Press any key to continue...\n");
	getchar();
	
	se->Distance[startR][startC]=0;
	se->ParentR[startR][startC]=-1;
	se->ParentC[startR][startC]=-1;

	q->enqueue(startR, startC);
	int currR, currC;
			//printf("hello\n");
	currR = startR;
	currC = startC;
	//printf("Rows: %d Cols: %d\n",rows,cols);
	//printf("queue size: %d\n",q->size());
			//printf("hello\n");
	while((q->size())!=0 && !(currR == goalR && currC == goalC))
	{

		//printf("Rows: %d Cols: %d\n",rows,cols);
		q->dequeue(currR, currC);
		
		//up
		if(g->data[currR-1][currC]==' ' && se->ParentR[currR-1][currC]==-2 && se->ParentC[currR-1][currC]==-2)
		{
			se->Distance[currR-1][currC] = se->Distance[currR][currC] + 1;
			se->ParentR[currR-1][currC] = currR;
			se->ParentC[currR-1][currC] = currC;
			q->enqueue(currR-1, currC);
			
			//draw movement
			g->data[currR-1][currC] = 'o';
			printWorld(g->data, rows, cols, screen, Rect, RectH, RectW);
			//system("pause");
			//getchar();
			delay();
		}
		
		//down
		if(g->data[currR+1][currC]==' ' && se->ParentR[currR+1][currC]==-2 && se->ParentC[currR+1][currC]==-2)
		{
			se->Distance[currR+1][currC] = se->Distance[currR][currC] + 1;
			se->ParentR[currR+1][currC] = currR;
			se->ParentC[currR+1][currC] = currC;
			q->enqueue(currR+1, currC);
			
			//draw movement
			g->data[currR+1][currC] = 'o';
			printWorld(g->data, rows, cols, screen, Rect, RectH, RectW);
			//system("pause");
			//getchar();
			delay();
		}
		
		//left
		if(g->data[currR][currC-1]==' ' && se->ParentR[currR][currC-1]==-2 && se->ParentC[currR][currC-1]==-2)
		{
			se->Distance[currR][currC-1] = se->Distance[currR][currC] + 1;
			se->ParentR[currR][currC-1] = currR;
			se->ParentC[currR][currC-1] = currC;
			q->enqueue(currR, currC-1);
			
			//draw movement
			g->data[currR][currC-1] = 'o';
			printWorld(g->data, rows, cols, screen, Rect, RectH, RectW);
			//system("pause");
			//getchar();
			delay();
		}
		
		//right
		if(g->data[currR][currC+1]==' ' && se->ParentR[currR][currC+1]==-2 && se->ParentC[currR][currC+1]==-2)
		{
			se->Distance[currR][currC+1] = se->Distance[currR][currC] + 1;
			se->ParentR[currR][currC+1] = currR;
			se->ParentC[currR][currC+1] = currC;
			q->enqueue(currR, currC+1);
			
			//draw movement
			g->data[currR][currC+1] = 'o';
			printWorld(g->data, rows, cols, screen, Rect, RectH, RectW);
			//system("pause");
			//getchar();
			SDL_Delay(50); 
		}
	}
	
	if(q->size()==0 && g->data[currR][currC]!='G')
	{
		se->Length=-1;
		return se;
	}
	else
	{
		se->Length=se->Distance[currR][currC];
		se->s->push(currR, currC);
		//se->s->
		while(currR != -1 && currC != -1)
		{
			se->s->push(se->ParentR[currR][currC], se->ParentC[currR][currC]);
			int tempR = currR, tempC=currC;
			currR=se->ParentR[tempR][tempC];
			currC=se->ParentC[tempR][tempC];	
		}		
		se->s->pop(currR, currC);
		return se;
	}
}

void printWorld(char** world, int rows, int cols, SDL_Surface* screen, SDL_Rect Rect, int RectH, int RectW){
    int i,j, x, y, h = 1, k = 1;;
    //system("cls");
    //printf("Start:\t %d %d\nGoal:\t %d %d\n", start.r, start.c, goal.r, goal.c );
	Optimise(screen);
	SDL_FreeSurface(screen);
	
	
    for(i = 0; i < rows; i++){
        for(j = 0; j < cols; j++){
            printf("%c", world[i][j]);
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
        
  		
  		SDL_Event event;
		SDL_PollEvent (&event);
		switch (event.type)
			{
				case SDL_QUIT:
					exit(0);
					break;
				case SDL_KEYDOWN:
			        printf( "Key press detected\n" );
			        exit(0);
			        break;
		
		      	case SDL_KEYUP:
			        printf( "Key release detected\n" );
			        exit(0);
			        break;
			    
			    case SDLK_KP_ENTER:
			    	printf( "Keypad Enter Detected\n" );
			    	exit(0);
			    	break;
					
				//case 
			}
			
		delay();
        Optimise(screen);
        //SDL_Flip(screen);
        if (SDL_Flip(screen) != 0) {
       		std::cerr << "Failed to swap the buffers: " << SDL_GetError() << std::endl;
  		}
        //system("pause");
        printf("\n");
    }
}

void Optimise(SDL_Surface* screen)
{
	SDL_Surface* temp = screen;
	//SDL_BlitSurface(temp, NULL, screen, NULL);
	//SDL_Flip(screen);
	//SDL_FreeSurface(temp);
	screen = SDL_DisplayFormat(temp);
	//SDL_Flip(temp);
	SDL_Flip(screen);
	//SDL_FreeSurface(screen);
	//SDL_FreeSurface(temp);
}

//Graphics stuff
void CreateRectangle(SDL_Surface* screen, SDL_Rect *Rect, int x, int y, int h, int w, int colour)
{
	//Set the dimesions of the rectangle
	Rect -> x = x;
	Rect -> y = y;
	Rect -> h = h;
	Rect -> w = w;
	//determine the colour required:
		//0 is for black
		//1 is for white
	switch(colour)
	{
		//black
		case 0:
		{
			//fill the rectangle
			SDL_FillRect(screen,Rect,SDL_MapRGB(screen -> format,0,0,0));
			break;
		} 
		//white
		case 1:
		{
			//fill the rectangle
			SDL_FillRect(screen,Rect,SDL_MapRGB(screen -> format,255,255,255));
			break;
		}
		//have other cases programmed for future changes like green and blue
		//red
		case 2:
		{
			//fill the rectangle
			SDL_FillRect(screen,Rect,SDL_MapRGB(screen -> format,255,0,0));
			break;
		}	
		//green
		case 3:
		{
			//fill the rectangle
			SDL_FillRect(screen,Rect,SDL_MapRGB(screen -> format,0,255,0));
			break;
		}	
		//blue
		case 4:
		{
			//fill the rectangle
			//if (rand()%2 == 1)
			//{
				SDL_FillRect(screen,Rect,SDL_MapRGB(screen -> format,0,0,255));
			//}
			//else
			//{
				//SDL_FillRect(screen,Rect,SDL_MapRGB(screen -> format,0,0,55));	
			//}				
			break;
		}
		
	}
}

/* --------------MISC FUNCTIONS--------------------- */
//these are fuctions I have used in FAC and DDS projects
//which I have found useful
int PowerFn (int no, int power)
{
	int j, ans;
	if (power == 0)
	{
		ans = 1;
	}
	else if (power == 1)
	{
		ans = no;
	}
	else
	{
		ans = no;
		for (j = 1; j < power; j++)
		{
			ans = ans*no;
		}
	}
	return ans;
}

void delay(){
   //long i = 0;
    //for(i = 0; i < 1000000L;i++); 
    //sleep(1000);
	//sleepy(1000L);
	//Sleep(1L);
	SDL_Delay(1);
	//sleepy;
}


