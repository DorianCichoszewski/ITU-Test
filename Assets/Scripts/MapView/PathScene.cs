using System;
using ITUTest.Pathfinding;

namespace ITUTest.MapView
{
	public class PathScene : IEquatable<PathScene>
	{
		public readonly NodeObject[] nodes;

		public readonly NodeObject start;
		public readonly NodeObject end;
		public readonly int cost = -1;

		public bool IsValid => cost >= 0;

		public PathScene(Path path, MapManager display)
		{
			cost = path.cost;
			start = display.GetNodeObject(path.start);
			end = display.GetNodeObject(path.end);

			nodes = new NodeObject[path.nodes.Length];
			for (int i = 0; i < path.nodes.Length; i++)
			{
				nodes[i] = display.GetNodeObject(path.nodes[i]);
			}
		}
		
		public static bool operator ==(PathScene path1, PathScene path2)
		{
			if (path1 is null)
				return path2 is null;
			return path1.Equals(path2);
		}

		public static bool operator !=(PathScene path1, PathScene path2)
		{
			return !(path1 == path2);
		}

		public bool Equals(PathScene other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			if (nodes.Length != other.nodes.Length) return false;
			for (int i = 0; i < nodes.Length; i++)
			{
				if (nodes[i] != other.nodes[i]) return false;
			}

			return true;
		}

		public override bool Equals(object obj)
		{
			return ReferenceEquals(this, obj) || obj is PathScene other && Equals(other);
		}

		public override int GetHashCode()
		{
			return (nodes != null ? nodes.GetHashCode() : 0);
		}
	}
}