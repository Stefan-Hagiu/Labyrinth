# Labyrinth

  This is a puzzle game made in Unity. The player is presented with a labyrinth, composed of black squares, which represent walls, white squares, which represent pathways, a blue square, which the player controls and a red square. 

  The objective is very simple: the player has to move the blue square until he or she reaches the red square.

  The heart of the game is the maze-creation algorithm, which was inspired from Prim's algorithm, a greedy algorithm in graph theory which takes a weighed undirected graph as input and outputs a minimum spanning tree of said graph. Because of the nature of this game, the actual implemented algorithm looks nothing like Prim's algorithm, but instead revolves around a few observations about the nature of 2D mazes. This is why the pathways in the maze actually form a tree. This means that between any 2 points, there is only 1 path.

  Update: I posted the game on newgrounds. http://www.newgrounds.com/portal/view/696834
