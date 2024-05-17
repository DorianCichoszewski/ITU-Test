using System.Collections.Generic;
using ITUTest.Pathfinding;
using ITUTest.Pathfinding.Algorithm;
using UnityEngine;

namespace ITUTest.MapView
{
	public class MapDisplay : MonoBehaviour
	{
		[SerializeField]
		private float nodeDistance = 1f;

		[SerializeField]
		private NodeObject nodePrefab;

		[SerializeField]
		private Transform nodesParentTransform;

		[SerializeField]
		private ModelOnPath modelController;

		private IPathfindingAlgorithm pathfinder;
		private readonly List<NodeObject> createdNodes = new();

		public void DisplayMap(Map generatedMap, NodesPool nodesPool)
		{
			foreach (var nodeGO in createdNodes)
			{
				Destroy(nodeGO.gameObject);
			}

			createdNodes.Clear();

			float startX = -generatedMap.Width * nodeDistance / 2f;
			float startY = -generatedMap.Height * nodeDistance / 2f;

			foreach (var node in generatedMap.nodes)
			{
				Vector3 position = new(startX + node.position.x * nodeDistance, 0,
					startY + node.position.y * nodeDistance);
				var newNode = Instantiate(nodePrefab, position, Quaternion.identity, transform);
				newNode.Init(generatedMap, nodesPool, generatedMap.GetIndex(node));
				createdNodes.Add(newNode);
			}
		}

		public void UpdateNodesOnPath(PathScene oldPath, PathScene newPath)
		{
			if (oldPath is { nodes: not null })
			{
				foreach (var nodeObject in oldPath.nodes)
				{
					nodeObject.IsPath = false;
				}
			}

			if (newPath is { nodes: not null })
			{
				foreach (var node in newPath.nodes)
				{
					node.IsPath = true;
				}
			}
		}

		public NodeObject GetNodeObject(int index)
		{
			return createdNodes[index];
		}
	}
}