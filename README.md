RapidXNA
========
@edg3

https://rapidxna.codeplex.com for original SVN

TODO
====
	-move TODOs to Issues
	-Clean up readme
	-Github Pages
	-link to Ernest's random repo
	-proper svn link or migration if possible
	-Move Codeplex Issues to Github Issues
	-Send Links To people involoved to join the GitHub Project
		https://rapidxna.codeplex.com/team/view

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
Each IGameScreen has overridable Load, Draw and Update methods, that correspond directly to the methods in your usual Game1.cs code. These are all called automatically in the background by the RapidEngine. They also each have a reference to Engine.

Using the ScreenService
3. Using Services
RapidXNA calls the major reusable classes that you would usually only need one instance of a "service". The engine will update and draw all services (so if you want to write an in app console you would most probably make it a service. You can write your own services by inheriting from IRapidService.

Using general Services
Features
This list is a work in progress

    Game state management via "screens"
    Asynchronous screen loading
    Built in Keyboard, GamePad and Mouse wrapping (Phone touch input wrapping in planning).

Structure
Overview of the major classes:

![alt tag](http://i40.tinypic.com/34qpooi.png)


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
Add in a way to add a static loading screen image for when loading a new screen, or a game screen to draw/update when currently loading.

Id #1000 | Release: None | Updated: Feb 1, 2013 at 6:01 AM by edg3 | Created: Jan 30, 2012 at 11:45 AM by edg3

