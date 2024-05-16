using System.Collections.Generic;

namespace ITUTest.Pathfinding.Algorithm
{
	public class BestFirstSearchPathfinder : BaseAlgorithm, IPathfindingAlgorithm
	{
		private readonly List<NodeCost> nodesToVisit = new();

		public void SetMap(Map map)
		{
			NewMap(map);
		}

		public Path FindPath(Node start, Node target)
		{
			SetupArrays();
			nodesToVisit.Clear();
			int finalCost = -1;

			nodesToVisit.Add(new NodeCost(start, 0));
			int startIndex = map.GetIndex(start);
			nodeVisited[startIndex] = true;
			nodeTravelCost[startIndex] = 0;

			// Find target
			while (nodesToVisit.Count > 0)
			{
				var current = GetLowestCost(nodesToVisit);
				var currentNode = current.node;

				if (currentNode == target)
				{
					finalCost = nodeTravelCost[map.GetIndex(currentNode)];
					break;
				}

				map.GetNearbyNodes(currentNode, ref nearbyNodes);
				foreach (var node in nearbyNodes)
				{
					if (nodeVisited[map.GetIndex(node.position)]) continue;

					nodeVisited[map.GetIndex(node.position)] = true;
					nodeTravelCost[map.GetIndex(node.position)] =
						nodeTravelCost[map.GetIndex(currentNode.position)] + 1;
					nodesToVisit.Add(new NodeCost(node, Heuristic(node, target)));
				}
			}

			if (finalCost == -1)
				return Path.InvalidPath();

			// Get path to start
			var pathNodes = new Node[finalCost + 1];
			pathNodes[0] = start;

			int currentCost = finalCost;
			var farthestNode = target;
			while (currentCost > 0)
			{
				pathNodes[currentCost] = farthestNode;
				map.GetNearbyNodes(farthestNode, ref nearbyNodes);
				foreach (var node in nearbyNodes)
				{
					if (nodeTravelCost[map.GetIndex(node.position)] < currentCost)
					{
						farthestNode = node;
						break;
					}
				}

				currentCost -= 1;
			}

			return new Path(start, target, pathNodes, finalCost);
		}

		private NodeCost GetLowestCost(List<NodeCost> list)
		{
			var minimalNodeCost = list[0];
			int minIndex = 0;
			for (int i = 1; i < list.Count; i++)
			{
				// Check for calculated cost first
				if (list[i].calculatedCost < minimalNodeCost.calculatedCost)
				{
					minimalNodeCost = list[i];
					minIndex = i;
				}
			}

			list.RemoveAt(minIndex);

			return minimalNodeCost;
		}

		private readonly struct NodeCost
		{
			public readonly Node node;
			public readonly int calculatedCost;

			public NodeCost(Node node, int calculatedCost)
			{
				this.node = node;
				this.calculatedCost = calculatedCost;
			}
		}
	}
}