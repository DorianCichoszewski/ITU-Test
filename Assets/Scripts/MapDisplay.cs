using System.Collections.Generic;
using ITUTest.Pathfinding;
using ITUTest.Pathfinding.Algorithm;
using UnityEngine;

namespace ITUTest
{
	public class MapDisplay : MonoBehaviour
	{
		[SerializeField]
		private int mapWidth, mapHeight;

		[SerializeField]
		private float nodeDistance = 1f;

		[SerializeField]
		private NodeGameObject nodePrefab;

		[SerializeField]
		private NodesPool nodesPool;
		
		[SerializeField]
		private Transform nodesParentTransform;

		private Map map;
		private IPathfindingAlgorithm pathfinder;
		private List<NodeGameObject> createdNodes = new();

		private void Start()
		{
			GenerateMap();
			pathfinder = new AStarPathfinder(map);

			Debug.Log(map);
			
			var n1 = map.GetRandomNode();
			Debug.Log(n1);
			var n2 = map.GetRandomNode();
			Debug.Log(n2);
			
			var path = pathfinder.FindPath(n1, n2);
			foreach (var pathNode in path.nodes)
			{
				createdNodes[map.GetIndex(pathNode)].SetPath(true);
			}
		}

		public void GenerateMap()
		{
			map = new Map(mapWidth, mapHeight);

			float startX = -mapWidth * nodeDistance / 2f;
			float startY = -mapHeight * nodeDistance / 2f;

			foreach (var node in map.nodes)
			{
				Vector3 position = new(startX + node.position.x * nodeDistance, 0,
					startY + node.position.y * nodeDistance);
				var newNode = Instantiate(nodePrefab, position, Quaternion.identity, transform);
				newNode.Init(map, nodesPool, node);
				createdNodes.Add(newNode);
			}
		}
	}
}