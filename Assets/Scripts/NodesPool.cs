using System.Collections.Generic;
using UnityEngine;

namespace ITUTest
{
	public class NodesPool : MonoBehaviour
	{
		[SerializeField]
		private GameObject nodePrefab;

		[SerializeField]
		private GameObject obstaclePrefab;

		[SerializeField]
		private GameObject pathPrefab;

		List<GameObject> nodes = new();
		List<GameObject> obstacles = new();
		List<GameObject> paths = new();

		public GameObject GetNode()
		{
			if (nodes.Count > 0)
				return GetFromList(nodes);
			else
				return Instantiate(nodePrefab);
		}

		public void ReturnNode(GameObject node)
		{
			node.SetActive(false);
			node.transform.SetParent(transform);
			nodes.Add(node);
		}
		
		public GameObject GetObstacle()
		{
			if (obstacles.Count > 0)
				return GetFromList(obstacles);
			else
				return Instantiate(obstaclePrefab);
		}

		public void ReturnObstacle(GameObject node)
		{
			node.SetActive(false);
			node.transform.SetParent(transform);
			obstacles.Add(node);
		}
		
		public GameObject GetPath()
		{
			if (paths.Count > 0)
				return GetFromList(paths);
			else
				return Instantiate(pathPrefab);
		}

		public void ReturnPath(GameObject node)
		{
			node.SetActive(false);
			node.transform.SetParent(transform);
			paths.Add(node);
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