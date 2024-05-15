using System.Collections.Generic;
using UnityEngine;

namespace ITUTest.Pathfinding.Algorithm
{
	public class AStarPathfinder : BaseAlgorithm, IPathfindingAlgorithm
	{
		private List<NodeCost> nodesToVisit;

		public AStarPathfinder(Map map) : base(map)
		{
			nodesToVisit = new((map.Width + map.Height) * 2);
		}
		
		public Path FindPath(Node start, Node target)
		{
			SetupArrays();
			nodesToVisit.Clear();
			int finalCost = -1;

			nodesToVisit.Add(new (start, 0, 69));

			// Find target
			while (true)
			{
				var current = GetLowestCost(nodesToVisit);
				var currentNode = current.node;
				int travelCost = current.travelCost;

				nodeVisited[currentNode.position.x, currentNode.position.y] = true;
				nodeCost[currentNode.position.x, currentNode.position.y] = travelCost;

				if (currentNode == target)
				{
					finalCost = travelCost;
					break;
				}

				map.GetNearbyNodes(currentNode, ref nearbyNodes);
				foreach (var node in nearbyNodes)
				{
					if (nodeVisited[node.position.x, node.position.y]) continue;

					nodeVisited[node.position.x, node.position.y] = true;
					nodesToVisit.Add(new (node, travelCost + 1, Heuristic(node, target)));
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