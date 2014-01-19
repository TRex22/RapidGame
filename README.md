RapidXNA
========
@edg3

https://rapidxna.codeplex.com for original SVN

About RapidXNA
==============
This is my attempt at migrating RapidXNA made by @edg3 to GitHub,
Upgrading it to work with MonoGame, and also getting it to work 
in its 2.0 Alpha version with XNA.

TODO
====
- move TODOs to Issues
- Clean up readme
- Github Pages
- link to Ernest's random repo
- proper svn link or migration if possible
- Move Codeplex Issues to Github Issues
- Send Links To people involoved to join the GitHub Project
	https://rapidxna.codeplex.com/team/view
- Migrate Histories of SVN files?
- May Leave on SVN, moving forward is more important
- Rename to something more friendly to XNA and MonoGame
- Fix up TODO Here with Task Lists Markdown
	https://github.com/blog/1375-task-lists-in-gfm-issues-pulls-comments
- Republish gh-pages

Original Notes:
===============


About RapidXNA
--------------
A simple framework that aims to make starting up new XNA projects for Windows, 
Xbox 360 and Windows Phone 7 easier. Using RapidXNA you should be able to 
easily port between the 3 platforms with only minor code changes to your projects.

News:
-----
RapidXNA 2.0 Alpha is available (it was meant to mimic functionality of 
RapidXNA 1 for now. If someone can confirm that RapidXNA 1 works on XBOX I can 
move it from Beta to Final release and focus on development of RapidXNA 2.0 that 
will hopefully in the end support Windows, Mac, Linux, Windows Phone 7, and Xbox 360. 
Furthermore, after RapidXNA 2.0 Release is ready I would like to look into extending to 
include Android and iOS eventually (if possible).

RapidXNA 2.0 worked out well, and much easier to use for the game development during 
Ludum Dare than I could have hoped for, but once again I got stumped by the Visual Studio 
built-in functions for creating "copy for" Windows Phone and the problem most likely propogates 
to the Xbox 360 version, so after some thought and sleep Im going to stop development of 
RapidXNA in favour of building a system that works in a unified way for 2D game dev on all 
platforms I want to support. I will post more about it when I get what I want to do right.

~~The RapidXNA 1 and 2.0 code will remain here though I will most probably not actively ~~
~~do development on the code any more. Should anyone want to take over the dev of RapidXNA ~~
~~send me a message/email and I can hand over project ownership.~~

Notes:
Please refer the the documentation for examples on how to use the framework.

Latest Changes:

    Added RapidEngine.Exit()
    Fixed accessibilty to RapidXNA.Service.ScreenService


Old Changes:

Projects using RapidXNA
Galactic Jump - coming soon

Last edited Apr 24, 2012 at 11:21 AM by edg3, version 6

Original Documentation
======================

How to use RapidXNA
1. Set up the project

    Add a reference to the .dll that corresponds with your current project
        Windows: RapidXNA.dll
        Xbox 360: RapidXNA360.dll
        Windows Phone 7: RapidXNAWP7.dll (supports Mango, 7.1 only)
    Remove the SpriteBatch related code and variables from Game1.cs
    Create a variable of type RapidXNA.RapidEngine, instantiate it in the LoadContent() method in your Game1.cs
    Add an Update call to your Update() method in Game1.cs for your instance of RapidXNA.RapidEngine's Update()
    Add a Draw call to your Draw() method in Game1.cs for your instance of RapidXNA.RapidEngine's Draw()
    Create a new class for your initial game screen (or state if you prefer) and inherit from RapidXNA.DataType.IGameScreen

2. Using "game screens"
Each IGameScreen has overridable Load, Draw and Update methods, that correspond directly to the methods in your usual Game1.cs code. 
These are all called automatically in the background by the RapidEngine. They also each have a reference to Engine.

Using the ScreenService
3. Using Services
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

Discussions From Codeplex
=========================

Suggestions 
-----------

 Wiki Link: [discussion:285185]
edg3
Coordinator
Jan 6, 2012 at 9:46 AM

	Please post all suggestions you have for addition to the framework here.
	
lancejz
Apr 17, 2012 at 12:43 AM
	

	How about a project template(s) in the download, for even more rapid XNA, as well as class templates.
	
edg3
Coordinator
Apr 17, 2012 at 6:49 AM
	

	Project templates make sense. What class templates do you think would make sense?

CodePlex Issues
===============

Loading wait time
-----------------
Add in a way to add a static loading screen image for when loading a new screen, or a game screen to 
draw/update when currently loading.

Id #1000 | Release: None | Updated: Feb 1, 2013 at 6:01 AM by edg3 | Created: Jan 30, 2012 at 11:45 AM by edg3

