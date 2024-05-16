using System.Collections.Generic;
using UnityEngine;

namespace ITUTest.Pathfinding.Algorithm
{
	public abstract class BaseAlgorithm
	{
		protected bool[] nodeVisited;
		protected int[] nodeTravelCost;
		protected Map map;
		
		protected List<Node> nearbyNodes;
		
		protected BaseAlgorithm(Map map)
		{
			this.map = map;
			nodeVisited = new bool[map.Width * map.Height];
			nodeTravelCost = new int[map.Width * map.Height];
			nearbyNodes = new (4);
		}
		
		protected void SetupArrays()
		{
			for (int x = 0; x < map.Width * map.Height; x++)
			{
				nodeTravelCost[x] = int.MaxValue;
				nodeVisited[x] = false;
			}
			
			nearbyNodes.Clear();
		}
		
		protected int Heuristic(Node node, Node target)
		{
			return Mathf.Abs(target.position.x - node.position.x) + Mathf.Abs(target.position.y - node.position.y);
		}
	}
}