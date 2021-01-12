AStar
=====

[![Build Status](https://travis-ci.org/valantonini/AStar.svg?branch=master)](https://travis-ci.org/valantonini/AStar)

An A Star (A*) algorithm for C# based on [Gustavo Franco's implementation](http://www.codeguru.com/csharp/csharp/cs_misc/designtechniques/article.php/c12527/AStar-A-Implementation-in-C-Path-Finding-PathFinder.htm).

The grid is represented as a 2d byte array (byte[,]). Blocked locations are marked as a 0, anything else is considered traversable.

Unlike a cartesian coordinate system that uses X for horizontal and Y for vertical on a 2D plane, the point used in this library represents a matrix index where X is the row and Y is the column (P<sub>xy</sub>)

Options allow for the use of diagonals and punishing direction changes.

    var pathfinderOptions = new PathFinderOptions {PunishChangeDirection = true};

    var pathfinder = new PathFinder(new byte[40,40], pathfinderOptions);
    
    var path = pathfinder.FindPath(new Point(1, 1), new Point(30, 30));


## Dependencies

The AStar Core implementation doesn't have any dependencies. The Tests and Vis use Unity.


## Unity Installation

Add this line to your Packages/manifest.json:

    "com.github.idbrii.unity-astar": "https://github.com/idbrii/unity-astar.git#latest-release",


## Visualization

The AStar.Vis module simplifies displaying path queries. You can set a grid of
SpriteRenderers to be your grid and use CellGrid to display queries.

* ExampleGrid - Placeholder for your implementation. Holds a byte[,] and a list of sprites that represent the grid.
* CellGrid - Manager for visualizing the grid using the list of sprites.
* DrawPathOnClick - Makes the grid clickable for easier demonstration.

You can use your own ICellVisual to control how grid cells are visualized or
use the provided CellSprite (2D) or CellImage (Canvas).

See the example scene: Runtime/AStar.Vis/example_pathfind_visualize.unity
