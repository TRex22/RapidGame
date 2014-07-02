Original Documentation for RapidXNA
===================================

How to use RapidXNA
0. Set up the project

    Add a reference to the .dll that corresponds with your current project
        Windows: RapidXNA.dll
        Xbox 360: RapidXNA360.dll
        Windows Phone 7: RapidXNAWP7.dll (supports Mango, 7.1 only)
    Remove the SpriteBatch related code and variables from Game1.cs
    Create a variable of type RapidXNA.RapidEngine, instantiate it in the LoadContent() method in your Game1.cs
    Add an Update call to your Update() method in Game1.cs for your instance of RapidXNA.RapidEngine's Update()
    Add a Draw call to your Draw() method in Game1.cs for your instance of RapidXNA.RapidEngine's Draw()
    Create a new class for your initial game screen (or state if you prefer) and inherit from RapidXNA.DataType.IGameScreen

1. Using "game screens"
Each IGameScreen has overridable Load, Draw and Update methods, that correspond directly to the methods in your usual Game1.cs code. 
These are all called automatically in the background by the RapidEngine. They also each have a reference to Engine.

Using the ScreenService
2. Using Services
RapidXNA calls the major reusable classes that you would usually only need one instance of a "service". 
The engine will update and draw all services (so if you want to write an in app console you would most probably 
make it a service. You can write your own services by inheriting from IRapidService.

Using general Services
Features
This list is a work in progress

    Game state management via "screens"
    Asynchronous screen loading
    Built in Keyboard, GamePad and Mouse wrapping (Phone touch input wrapping in planning).

Structure
Overview of the major classes:

![alt tag](http://i40.tinypic.com/34qpooi.png)

Services
========

A service is a single instance of an object that gets updated, drawn, and loaded as needed. 
Services arent asynchronously loaded. You can only have one instance of each 
possible service class in the Services stack at any given time.

Adding a service
----------------
	Engine.AddService(...)

Using a service
---------------
	Engine.ServiceOf<Type>()
Which returns the first instance of a service of the type "Type", it will return default(T) 
if it does not find that service in the stack.

	Last edited Jan 3, 2012 at 3:34 PM by edg3, version 1

ScreenService
=============

What is the ScreenService?
--------------------------
The ScreenService is the only service the Engine starts up by default, it manages your game state 
and what to draw and update when.

What is a Game Screen?
----------------------
A game screen is a "state" of your game, this could be a menu, a settings page, a network lobby, 
in-game, etc. This splits the code from your major game states into separate "screens". 
Only the "top" screen is drawn.

What is a Popup Screen?
-----------------------
A popup screen is exaclty the same as a game screen, but is handled differently, if there is a popup 
screen in the stack the current game screen will not update, but any popup screen drawing will 
occur on top of the main game screen. Popup screens can be used for in game pause screens, dialogue popups, 
and other actions that should pause the game state of the main game window.

How do I gain access to the ScreenService?
------------------------------------------
Using:
	Engine.ServiceOf<ScreenService>().[...]
Where ... is a specific function. To move to a new game screen use PushGameScreen(...), to move back to 
the previous use PopGameScreen(), if you pop a game screen you lose its state, so keep this in mind 
(ie. this is when you would want to save data, etc).

	Last edited Jan 3, 2012 at 3:31 PM by edg3, version 1
