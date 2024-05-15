# ITU-Test
Unity version 2022.3.20f1. Please use only built-in Unity components. Use 3D Mode for this task.
Create a simple pathfinding demo:
- the map should consist of square tiles that are either traversable (1) or an obstacle (0) and create an orthogonally connected grid (i.e. each inner tile has 4 neighbours, edge tile has 3 neighbours and corner tile has 2 neighbours). you can move from each tile in one of 4 directions (N, S, E, W) unless you're on an edge of the map or there's an obstacle blocking your way. each move between neighbouring tiles has the same "cost" (1).
- the goal of the exercise is to use a pathfinding algorithm best suited for the task (fastest computing time). please explain what algorithm you've chosen and why? if you've made any adjustments to its standard implementation also explain what and why?
The end user should be able to:
- adjust the size of the map (either via a config file or during runtime - i.e. in an edit mode of the demo)
- adjust the placement of obstacles (preferably during runtime)
- choose a start and end point on the map and be shown a optimal path between the two tiles
- freely look around the map
- for extra credit: make the default Unity character model run the path on the map
