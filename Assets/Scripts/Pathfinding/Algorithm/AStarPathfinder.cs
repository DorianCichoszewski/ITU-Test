using System.Collections.Generic;
using UnityEngine;

namespace ITUTest.Pathfinding.Algorithm
{
	public class AStarPathfinder : IPathfindingAlgorithm
	{
		private bool[,] nodeVisited;
		private int[,] nodeCost;
		private Map map;
		
		private List<NodeCost> nodesToVisit;
		private List<Node> nearbyNodes;

		public AStarPathfinder(Map map)
		{
			this.map = map;
			nodeVisited = new bool[map.Width, map.Height];
			nodeCost = new int[map.Width, map.Height];
			nodesToVisit = new((map.Width + map.Height) * 2);
			nearbyNodes = new(4);
		}
		
		public Path FindPath(Node start, Node target)
		{
			SetupArrays();
			int finalCost = -1;

			nodesToVisit.Add(new (start, 0, 69));

			// Find target
			while (true)
			{
				var current = GetLowestCost(nodesToVisit);
				var currentNode = current.node;
				int cost = current.travelCost;

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
					int calculatedCost = Mathf.Abs(target.position.x - node.position.x) +
										 Mathf.Abs(target.position.y - node.position.y);
					nodesToVisit.Add(new (node, cost + 1, calculatedCost));
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
		
		private struct NodeCost
		{
			public readonly Node node;
			public readonly int travelCost;
			public readonly int calculatedCost;

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