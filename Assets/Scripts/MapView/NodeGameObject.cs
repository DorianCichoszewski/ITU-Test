using ITUTest.Pathfinding;
using UnityEngine;

namespace ITUTest.MapView
{
	public class NodeGameObject : MonoBehaviour, ICameraInputTaker
	{
		private Map map;
		private int nodeIndex;
		private NodesPool nodesPool;

		private GameObject createdNode;
		private GameObject createdObstacle;
		private GameObject createdPath;

		public void Init(Map map, NodesPool pool, int index)
		{
			this.map = map;
			nodeIndex = index;
			nodesPool = pool;

			SetNode();
		}
		
		public void MousePressed()
		{
			var type = map.GetNode(nodeIndex).type == NodeType.Traversable ? NodeType.Obstacle : NodeType.Traversable;
			map.ChangeNodeType(nodeIndex, type);
			SetNode();
		}

		public void SetPath(bool isPath)
		{
			if (isPath)
			{
				if (createdPath != null) return;

				createdPath = nodesPool.GetPath();
				createdPath.transform.SetParent(transform);
				createdPath.transform.localPosition = Vector3.zero;
			}
			else
			{
				if (createdPath == null) return;

				nodesPool.ReturnPath(createdPath);
				createdPath = null;
			}
		}

		private void OnDestroy()
		{
			ReturnNode();

			if (createdPath)
				nodesPool.ReturnPath(createdPath);
		}

		private void SetNode()
		{
			ReturnNode();
			Transform createdObject;
			
			if (map.GetNode(nodeIndex).type == NodeType.Traversable)
			{
				createdNode = nodesPool.GetNode();
				createdObject = createdNode.transform;
			}
			else
			{
				createdObstacle = nodesPool.GetObstacle();
				createdObject = createdObstacle.transform;
			}
			
			createdObject.SetParent(transform);
			createdObject.localPosition = Vector3.zero;
		}

		private void ReturnNode()
		{
			if (createdNode)
				nodesPool.ReturnNode(createdNode);
			if (createdObstacle)
				nodesPool.ReturnObstacle(createdNode);
		}
	}
}