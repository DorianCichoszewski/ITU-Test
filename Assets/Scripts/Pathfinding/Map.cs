using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace ITUTest.Pathfinding
{
	public class Map
	{
		public readonly Node[] nodes;

		public int Width { get; }
		public int Height { get; }

		public Node GetNode(int x, int y) => GetNode(new Vector2Int(x, y));
		public Node GetNode(Vector2Int coordinates) => nodes[GetIndex(coordinates)];
		public Node GetNode(int index) => nodes[index];
		
		public Map(int width, int height)
		{
			nodes = new Node[width * height];
			Width = width;
			Height = height;

			GenerateRandomMap();
		}

		public void GetNearbyNodes(Node node, ref List<Node> nearbyNodes)
		{
			nearbyNodes.Clear();

			if (node.position.y - 1 >= 0)
			{
				var nearbyNode = GetNode(node.position.x, node.position.y - 1);
				if (nearbyNode.type == NodeType.Traversable)
					nearbyNodes.Add(nearbyNode);
			}
			if (node.position.y + 1 < Height)
			{
				var nearbyNode = GetNode(node.position.x, node.position.y + 1 );
				if (nearbyNode.type == NodeType.Traversable)
					nearbyNodes.Add(nearbyNode);
			}
			if (node.position.x - 1 >= 0)
			{
				var nearbyNode = GetNode(node.position.x - 1, node.position.y);
				if (nearbyNode.type == NodeType.Traversable)
					nearbyNodes.Add(nearbyNode);
			}
			if (node.position.x + 1 < Width)
			{
				var nearbyNode = GetNode(node.position.x + 1, node.position.y);
				if (nearbyNode.type == NodeType.Traversable)
					nearbyNodes.Add(nearbyNode);
			}
		}

		public Node GetRandomNode(NodeType type = NodeType.Traversable)
		{
			while (true)
			{
				var x = Random.Range(0, Width);
				var y = Random.Range(0, Height);
				if (GetNode(x, y).type == type)
				{
					return GetNode(x, y);
				}
			}
		}
		
		public Vector2Int GetCoordinatesFromIndex(int index)
		{
			var x = index % Width;
			var y = index / Width;
			return new(x, y);
		}
		
		public int GetIndex(Vector2Int coordinates)
		{
			return coordinates.y * Width + coordinates.x;
		}
		
		public int GetIndex(Node node)
		{
			return node.position.y * Width + node.position.x;
		}

		public override string ToString()
		{
			var stringBuilder = new StringBuilder();

			for (int x = 0; x < Width; x++)
			{
				for (int y = 0; y < Height; y++)
				{
					stringBuilder.Append((int)GetNode(x, y).type);
					stringBuilder.Append(", ");
				}

				stringBuilder.Append("\n");
			}

			return stringBuilder.ToString();
		}

		// Random Distribution of obstacles. Only checks if at least 2 nodes are traversable (for testing)
		private void GenerateRandomMap()
		{
			int traversableCount;
			do
			{
				traversableCount = 0;
				for (int x = 0; x < Width * Height; x++)
				{
					var type = Random.Range(0f, 1f) < 0.3f ? NodeType.Obstacle : NodeType.Traversable;
					if (type == NodeType.Traversable)
						traversableCount += 1;
					nodes[x] = new Node(GetCoordinatesFromIndex(x), type);
				}
			} while (traversableCount < 2);
		}
	}
}