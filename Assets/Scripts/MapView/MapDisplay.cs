using System;
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
		private NodesPool nodesPool;

		[SerializeField]
		private Transform nodesParentTransform;

		private IPathfindingAlgorithm pathfinder;
		private readonly List<NodeObject> createdNodes = new();

		private Path currentPath;

		public event Action<Map> OnMapGenerated;

		public Map GeneratedMap { get; private set; }

		public void GenerateMap(int width, int height, MapGenerationMode mode)
		{
			foreach (var nodeGO in createdNodes)
			{
				Destroy(nodeGO.gameObject);
			}

			createdNodes.Clear();
			currentPath = null;

			GeneratedMap = new Map(width, height, mode);

			float startX = -width * nodeDistance / 2f;
			float startY = -height * nodeDistance / 2f;

			foreach (var node in GeneratedMap.nodes)
			{
				Vector3 position = new(startX + node.position.x * nodeDistance, 0,
					startY + node.position.y * nodeDistance);
				var newNode = Instantiate(nodePrefab, position, Quaternion.identity, transform);
				newNode.Init(GeneratedMap, nodesPool, GeneratedMap.GetIndex(node));
				createdNodes.Add(newNode);
			}

			OnMapGenerated?.Invoke(GeneratedMap);
		}

		public void UpdateNodesOnPath(Path newPath)
		{
			if (currentPath != null)
			{
				foreach (var node in currentPath.nodes)
				{
					var nodeObject = createdNodes[GeneratedMap.GetIndex(node)];
					nodeObject.IsPath = false;
				}
			}

			if (newPath != null)
			{
				foreach (var node in newPath.nodes)
				{
					var nodeObject = createdNodes[GeneratedMap.GetIndex(node)];
					nodeObject.IsPath = true;
				}
			}
			currentPath = newPath;
		}
	}
}