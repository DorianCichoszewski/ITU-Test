using System;
using System.Collections.Generic;
using UnityEngine;

namespace ITUTest.MapView
{
	public class NodesPool : MonoBehaviour
	{
		[SerializeField]
		private GameObject nodePrefab;

		[SerializeField]
		private GameObject obstaclePrefab;

		[SerializeField]
		private GameObject pathPrefab;

		private readonly Dictionary<NodeObjectType, List<GameObject>> nodesPool = new();

		private void Awake()
		{
			nodesPool.Add(NodeObjectType.Traversable, new List<GameObject>());
			nodesPool.Add(NodeObjectType.Obstacle, new List<GameObject>());
			nodesPool.Add(NodeObjectType.Path, new List<GameObject>());
		}

		public GameObject GetNode(NodeObjectType type)
		{
			if (nodesPool[type].Count > 0)
				return GetFromList(nodesPool[type]);
			return type switch {
				NodeObjectType.Traversable => Instantiate(nodePrefab),
				NodeObjectType.Obstacle => Instantiate(obstaclePrefab),
				NodeObjectType.Path => Instantiate(pathPrefab),
				_ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
			};
		}

		public void ReturnNode(NodeObjectType type, GameObject node)
		{
			if (node == null)
				return;

			// To prevent bugs on closing app
			if (this == null)
				return;

			nodesPool[type].Add(node);

			node.SetActive(false);
			node.transform.SetParent(transform);
		}

		private GameObject GetFromList(List<GameObject> list)
		{
			var obj = list[^1];
			list.RemoveAt(list.Count - 1);

			obj.SetActive(true);
			return obj;
		}
	}
}