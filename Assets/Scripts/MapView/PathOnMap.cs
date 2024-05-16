using ITUTest.MapView;
using ITUTest.Pathfinding;
using ITUTest.Pathfinding.Algorithm;
using UnityEngine;

namespace ITUTest
{
	public class PathOnMap : MonoBehaviour
	{
		[SerializeField]
		private MapDisplay mapDisplay;
		[SerializeField]
		private NodeRaycast raycaster;
		
		[SerializeField]
		private GameObject startMarker;
		[SerializeField]
		private GameObject endMarker;
		
		private IPathfindingAlgorithm pathfinder = new BestFirstSearchPathfinder();
		
		private NodeObject startNodeObject;
		private NodeObject endNodeObject;

		private void Start()
		{
			mapDisplay.OnMapGenerated += map =>
			{
				pathfinder.SetMap(map);
				startMarker.SetActive(false);
				endMarker.SetActive(false);
				startNodeObject = null;
				endNodeObject = null;
			};
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
			
			var startNode = mapDisplay.GeneratedMap.GetNode(startNodeObject.NodeIndex);
			var endNode = mapDisplay.GeneratedMap.GetNode(endNodeObject.NodeIndex);
			
			var path = pathfinder.FindPath(startNode, endNode);
			mapDisplay.UpdateNodesOnPath(path);
		}

		private bool IsNodeTraversable(NodeObject node)
		{
			return mapDisplay.GeneratedMap.GetNode(node.NodeIndex).type == NodeType.Traversable;
		}
	}
}