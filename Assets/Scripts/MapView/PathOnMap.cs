using ITUTest.Pathfinding;
using ITUTest.Pathfinding.Algorithm;
using UnityEngine;

namespace ITUTest.MapView
{
	public class PathOnMap : MonoBehaviour
	{
		[SerializeField]
		private MapManager mapManager;
		[SerializeField]
		private NodeRaycast raycaster;
		
		[SerializeField]
		private GameObject startMarker;
		[SerializeField]
		private GameObject endMarker;
		
		private IPathfindingAlgorithm pathfinder = new AStarPathfinder();
		
		private NodeObject startNodeObject;
		private NodeObject endNodeObject;

		public void ResetMap(Map map)
		{
			pathfinder.SetMap(map);
			startMarker.SetActive(false);
			endMarker.SetActive(false);
			startNodeObject = null;
			endNodeObject = null;
		}

		public void SetStartPoint(NodeObject node)
		{
			if (!IsNodeTraversable(node))
				return;
			startNodeObject = node;
			startMarker.SetActive(true);
			startMarker.transform.position = node.transform.position;
			CalculatePath();
		}

		public void SetEndPoint(NodeObject node)
		{
			if (!IsNodeTraversable(node))
				return;
			endNodeObject = node;
			endMarker.SetActive(true);
			endMarker.transform.position = node.transform.position;
			CalculatePath();
		}

		public void CalculatePath()
		{
			if (startNodeObject == null || endNodeObject == null)
				return;

			var startNode = mapManager.GetNode(startNodeObject);
			var endNode = mapManager.GetNode(endNodeObject);
			
			var path = pathfinder.FindPath(startNode, endNode);
			mapManager.UpdatePath(path);
		}

		private bool IsNodeTraversable(NodeObject node)
		{
			return mapManager.GeneratedMap.GetNode(node.NodeIndex).type == NodeType.Traversable;
		}
	}
}