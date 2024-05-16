using System.Collections.Generic;

namespace ITUTest.Pathfinding.Algorithm
{
	public class AStarPathfinder : BaseAlgorithm, IPathfindingAlgorithm
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

			nodesToVisit.Add(new NodeCost(start, 0, 69));
			nodeVisited[map.GetIndex(start)] = true;

			// Find target
			while (true)
			{
				var current = GetLowestCost(nodesToVisit);
				var currentNode = current.node;
				int travelCost = current.travelCost;

				nodeTravelCost[map.GetIndex(currentNode.position)] = travelCost;

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
					nodesToVisit.Add(new NodeCost(node, travelCost + 1, Heuristic(node, target)));
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
				if (list[i].FinalCost < minimalNodeCost.FinalCost)
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
			public readonly int travelCost;
			private readonly int calculatedCost;

			public int FinalCost => travelCost + calculatedCost;

			public NodeCost(Node node, int travelCost, int calculatedCost)
			{
				this.node = node;
				this.travelCost = travelCost;
				this.calculatedCost = calculatedCost;
			}
		}
	}
}