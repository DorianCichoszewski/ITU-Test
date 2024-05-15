using System.Collections.Generic;

namespace ITUTest.Pathfinding.Algorithm
{
	// As cost is always 1 it's the same as breadth first search
	public class DijkstraPathfinder : IPathfindingAlgorithm
	{
		private bool[,] nodeVisited;
		private int[,] nodeCost;
		private Map map;

		private Queue<NodeCost> nodesToVisit;
		private List<Node> nearbyNodes;

		public DijkstraPathfinder(Map map)
		{
			this.map = map;
			nodeVisited = new bool[map.Width, map.Height];
			nodeCost = new int[map.Width, map.Height];
			nodesToVisit = new((map.Width + map.Height) * 2);
			nearbyNodes = new (4);
		}

		public Path FindPath(Node start, Node target)
		{
			SetupArrays();
			int finalCost = -1;

			nodesToVisit.Enqueue(new(start, 0));

			// Find target
			while (true)
			{
				var current = nodesToVisit.Dequeue();
				var currentNode = current.node;
				int cost = current.cost;

				nodeVisited[currentNode.position.x, currentNode.position.y] = true;
				nodeCost[currentNode.position.x, currentNode.position.y] = cost;

				if (currentNode == target)
				{
					finalCost = cost;
					break;
				}

				map.GetNearbyNodes(currentNode, ref nearbyNodes);
				foreach (var node in nearbyNodes)
				{
					if (nodeVisited[node.position.x, node.position.y]) continue;

					nodeVisited[node.position.x, node.position.y] = true;
					nodesToVisit.Enqueue(new(node, cost + 1));
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
					if (nodeCost[node.position.x, node.position.y] < currentCost)
					{
						farthestNode = node;
						break;
					}
				}

				currentCost -= 1;
			}

			return new Path(start, target, pathNodes, finalCost);
		}

		private void SetupArrays()
		{
			for (int x = 0; x < map.Width; x++)
			{
				for (int y = 0; y < map.Height; y++)
				{
					nodeCost[x, y] = int.MaxValue;
					nodeVisited[x, y] = false;
				}
			}

			nodesToVisit.Clear();
			nearbyNodes.Clear();
		}

		private struct NodeCost
		{
			public readonly Node node;
			public readonly int cost;

			public NodeCost(Node node, int cost)
			{
				this.node = node;
				this.cost = cost;
			}
		}
	}
}