In Progress...

# Game Of Life in 3D
Conway's Game Of Life made in 3D with adjustable rules.
Made with Unity 3D in C#.

## Editor
Run the current scene (on play mode).

In the scene Hierarchy, Go to the GameOfLife3D GameObject's inspector > Right click on the Game Of Life Controller component > Game Of Life. 
You will have a bunch of functions to control your simulation.
Use them only while in play mode.

You can twick the values of the script and change the grid size inside the CellManager component.

## Example
Grid size: 10x10x10

Rules:
- Minimum of alive neighbours for cell to survive: 3
- Maximum amount of alive neighbours for cell to survive: 4
- Amount of alive neighbours to live: 4

Initial pattern: Block3D

Initial pattern position: (5,5,5)

![](./Images/gameoflife3d.gif)
