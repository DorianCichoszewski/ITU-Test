using System.Collections.Generic;

namespace ITUTest.Pathfinding.Algorithm
{
	// As cost is always 1 it's the same as breadth first search
	public class DijkstraPathfinder : BaseAlgorithm, IPathfindingAlgorithm
	{
		private Queue<Node> nodesToVisit;

		public DijkstraPathfinder(Map map) : base(map)
		{
			nodesToVisit = new((map.Width + map.Height) * 2);
		}

		public Path FindPath(Node start, Node target)
		{
			SetupArrays();
			nodesToVisit.Clear();
			int finalCost = -1;

			nodesToVisit.Enqueue(start);
			nodeVisited[map.GetIndex(start)] = true;

			// Find target
			while (true)
			{
				var currentNode = nodesToVisit.Dequeue();

				if (currentNode == target)
				{
					finalCost = nodeTravelCost[map.GetIndex(currentNode)];
					break;
				}

				map.GetNearbyNodes(currentNode, ref nearbyNodes);
				foreach (var node in nearbyNodes)
				{
					if (nodeVisited[map.GetIndex(node)]) continue;

					nodeVisited[map.GetIndex(node)] = true;
					nodeTravelCost[map.GetIndex(node)] = nodeTravelCost[map.GetIndex(currentNode)] + 1;
					nodesToVisit.Enqueue(node);
				}
			}

			if (finalCost == -1)
				return Path.InvalidPath();

			// Get path to start
			Node[] pathNodes = new Node[finalCost + 1];
			pathNodes[0] = start;

			int currentCost = finalCost;
			var farthestNode = target;
			while (currentCost > 0)
			{
				pathNodes[currentCost] = farthestNode;
				map.GetNearbyNodes(farthestNode, ref nearbyNodes);
				foreach (var node in nearbyNodes)
				{
					if (nodeTravelCost[map.GetIndex(node)] < currentCost)
					{
						farthestNode = node;
						break;
					}
				}

				currentCost -= 1;
			}

			return new Path(start, target, pathNodes, finalCost);
		}
	}
}