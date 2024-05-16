namespace ITUTest.Pathfinding.Algorithm
{
	public interface IPathfindingAlgorithm
	{
		public void SetMap(Map map);

		public Path FindPath(Node start, Node target);
	}
}