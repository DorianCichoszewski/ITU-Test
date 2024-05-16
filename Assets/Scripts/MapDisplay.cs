using System.Collections.Generic;
using ITUTest.Pathfinding;
using ITUTest.Pathfinding.Algorithm;
using UnityEngine;

namespace ITUTest
{
	public class MapDisplay : MonoBehaviour
	{
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

		public void GenerateMap(int width, int height)
		{
			map = new Map(width, height);

			float startX = -width * nodeDistance / 2f;
			float startY = -height * nodeDistance / 2f;

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