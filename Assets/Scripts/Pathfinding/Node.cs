using System;
using UnityEngine;

namespace ITUTest.Pathfinding
{
	public struct Node : IEquatable<Node>
	{
		public readonly Vector2Int position;
		public NodeType type;

		public Node(Vector2Int position, NodeType type)
		{
			this.position = position;
			this.type = type;
		}

		public static bool operator ==(Node node1, Node node2)
		{
			return node1.Equals(node2);
		}

		public static bool operator !=(Node node1, Node node2) => !(node1 == node2);

		public bool Equals(Node other)
		{
			return position.Equals(other.position);
		}

		public override bool Equals(object obj)
		{
			return obj is Node other && Equals(other);
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(position, (int)type);
		}

		public override string ToString()
		{
			return $"{position}, {type}";
		}
	}
}