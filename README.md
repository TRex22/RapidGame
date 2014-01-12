RapidGame
========
@edg3

https://rapidxna.codeplex.com for original SVN

About RapidGame
==============
This is my attempt at migrating RapidXNA made by @edg3 to GitHub,
Upgrading it to work with MonoGame, and also getting it to work 
in its 2.0 Alpha version with XNA, since I dont think that version
was functional.

Releases
========
They are found at:
http://TRex22.github.io/RapidGame/Releases

There are preliminary releases which are perhaps kind of useless, they are
untested but released so that if they are needed they may be used.

Project Specifications
======================
Releases will be stored on the gh-pages.

All new code is pushed to develop and pull/merge requests sent for
	merging with master.
Other changes can get their own branches
This follows a successful git branching model discussed here:
	http://nvie.com/posts/a-successful-git-branching-model/

The static information page will be used for the developer wiki,
feeds and general information stuff.
This will only be implemented when I have done that project
(It is another project of mine, to build a boilerplate for gh-pages)
For now the built in GitHub wiki will be used.

Contributions are welcome, but first I would like prospective contributers
to either create their own branch (after sending me a message) or fork the
project and then contribute. Afterwards send pull/merge requests to develop.

Milestones
==========

Milestone 1
-----------
The first milestone of the project is to setup a function project outline,
complete the RapidXNA 2 Alpha and complete the migration from the SVN repo.

Requirements
============

To Effectively Load and Build the Source or a Project
-----------------------------------------------------
RapidXNA requires XNA Game Studio, Visual Studio (c#) 2010 and Windows Phone 
sdk 7.

RapidGame and Mono Related projects will require MonoGame 3 for VS 2012, 
although MonoDevelop could also be used if desired. Also the Mono SDK is needed.

To Run a Project
----------------
RapidXNA Requires the XNA Redistributable for Windows.
RapidGame will require OpenAl, and other related Redistributables, such
as DirectX, OpenGL, Unity ect...
