using System.Text;

namespace ITUTest.Pathfinding
{
	public class Path
	{
		public readonly Node[] nodes;

		public readonly Node start;
		public readonly Node end;
		public readonly int cost = -1;

		public bool IsValid => cost >= 0;

		public Path(Node start, Node end, Node[] nodes, int cost)
		{
			this.start = start;
			this.end = end;
			this.cost = cost;
			this.nodes = nodes;
		}

		// Constructor for invalid path
		private Path() { }

		public static Path InvalidPath()
		{
			return new Path();
		}

		public override string ToString()
		{
			if (!IsValid) return $"Invalid path from {start} to {end}";
			
			var stringBuilder = new StringBuilder();
			stringBuilder.AppendLine($"Path from {start} to {end} costs {cost}:");
			foreach (var node in nodes)
			{
				stringBuilder.AppendLine(node.ToString());
			}

			return stringBuilder.ToString();
		}
	}
}