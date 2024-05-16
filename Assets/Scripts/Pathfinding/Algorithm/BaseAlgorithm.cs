using System.Collections.Generic;
using UnityEngine;

namespace ITUTest.Pathfinding.Algorithm
{
	public abstract class BaseAlgorithm
	{
		protected bool[] nodeVisited;
		protected int[] nodeTravelCost;
		protected Map map;

		protected List<Node> nearbyNodes = new(4);

		protected void NewMap(Map map)
		{
			if (this.map != null &&
				map.Height == this.map.Height &&
				map.Width == this.map.Width)
			{
				this.map = map;
				return;
			}

			this.map = map;
			nodeVisited = new bool[map.Width * map.Height];
			nodeTravelCost = new int[map.Width * map.Height];
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