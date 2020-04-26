# Newtonian-PhageShift
#### CPSC6830 - Game Programming Final Project - Phage Shift game.
#### Members: Benjamin Warner, Derek Andrews, Hayden Lewis

[Check out our 1st build!!!](https://teamnewtonian.github.io/phageshift/build-web1)

[Check out our 2nd build!!!  Now with scores?](https://teamnewtonian.github.io/phageshift/build-web2)

[Check out our blog!](https://teamnewtonian.github.io/phageshift/blog)

![Bacteriphages in their natural environment](https://teamnewtonian.github.io/phageshift/images/phage2.PNG)

### What is the goal of the game? What is the story or theme or emotion?

You play as a bacteriophage (the Phage) re-engineered for good. Your mission is to eliminate the virus that has
spread throughout the mysterious being referred to simply as "the Host". It is unknown exactly why you were created or
who the host is at this point, but you are only driven by the need to purge the enemies... or disintegrate trying. The theme is
energetic survival, with an underlying mystery as to where you are. Stay alive and eliminate as much of the virus to see if
there is an end to this journey.

### Ideas on style and design. What does the game world look like?

The world as you know it cosists of sections of some organic being, with a minimalist and stylized design. Each
floor that you encounter is different from the last, but there is no discernable pattern. Walls block your path, hazards slow
or harm you, and viruses lurk in growing numbers. Clear the floor to open a pathway down to the next level, until you
reach the core. Clear all floors to win, or leave a score if you don't make it.

### How does the player control the game? Is there a user interface?

Control the Phage with WASD to move around the map. Mouse over to aim and click to shoot. Use QE to rotate
the camera, P to pause, and ESC to quit.
There is a title screen with a start and quit button and previous highscores listed.
A pause screen is available with controls information. A limited HUD shows the timer value and at least shows how to
pause (ex: "P to pause"). If time runs out, you are overrun and lose.

### What other elements are there (NPCs, collectibles, dangers)?

In addition to the virus hovering around the playfield, some power-ups and environmental dangers will appear.
Enemies will float around randomly and follow the player when in range. Power-ups can be found around the field to
increase firerate or damage. The playfield is obstructed by walls, and some walls have spikes. Beware! Other cautions
include high speed currents when there are no walls to control flow, this will make movement difficult but not impossible.
The player must try not to fall off the edge.

### What scripts need to be programmed?

The three main pillars of scripts that we will need to develop are controller script, AI script, and level generation
script. The controller script would control movement, shooting, and other keyboard inputs associated with player. The AI
script controlling the movement, spawning, and behavior of our enemies. Finally the level generation script that will
generate the level in a procedural nature with our assets. These are the main pillars of which the majority of code will be
going. Other miscellaneous scripts would be the camera , health, UI, and the game manager script. The game manager
script will hold various pieces of information from win conditions to pause menu to score.

### Think through the player’s experience and interest curve. What is your minimum playable game?

The player will be assumed to have no experience in the start of the game. Through various levels the player will
gain experience and knowledge of the game world and the levels will be increasingly be more difficult. This process will
keep the interest curve of the experienced player. The minimum playable game will be the antibody shooting at the fixed
number of enemies that are randomly spawned in our world. The minimum game will not have level progression just a
fixed camera with our character defending itself until enemies or characters death.

### Who has ownership for which scripts and assets and which parts of the game?

The project will be broken up into 3 main sections: Player Character; and Enemies; Map Generation. Derek will
be working on the player character’s controls, with its related scripts and assets, including the shooting functionality.
Hayden will work on the enemy characters and their related assets and scripts. Benjamin will develop the map and
procedural map generation for the game. Any additional functionality that can’t be put into those categories will be
handled as we come across it, as we currently have some extra flexibility with times that we are available.

### Split into what is provided at minimum and possible extensions. What do you plan to do for each build?

For our first build, we will have the player character’s and the enemies’ basic functionality working, allowing
them to move and attack at minimum. We will have some form of level generation, though this can easily be simplified for
a prototype build and improve as we progress. Complexities will be added after the first build, including power-ups,
collectibles, aesthetics, and improved map generation.

### When are your regular meetings times?

So far, we’ve met at a couple of different times since we are all working from home and can easily meet and share
screens through Zoom.

### What did we learn?
We learned various aspects of game devolopment and teamwork. We learned that unity is a massive engine that can be used to create anything.
We learned how to use scripts to instantiate game objects in a procedural manner. 
Animation and blending animation to be modified in unity. How to efficently manage a game with resources.
The majority of this game we had to learn something new for.

### What is great?
The game. We spent alot of our free time on making this a game we wanted to play and be a complete game. 
We are proud of our product and the work we put into it.

### Thank you Dr.Joerg and Alex we appreciate your time in helping us and providing feedback.


### Sources:
Textures:
Textures.com
substanceshare.com

Sound:
https://freemusicarchive.org/music/BoxCat_Games - BoxCat Games

Other: 
Procedural Animation - Unity Tutorial for phage legs by Ironstar Interactive 
