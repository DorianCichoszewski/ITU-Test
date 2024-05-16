using ITUTest.Pathfinding;
using UnityEngine;

namespace ITUTest.MapView
{
	public class NodeObject : MonoBehaviour
	{
		private Map map;
		private NodesPool nodesPool;
		private bool isPath;

		private NodeObjectType currentType;
		private GameObject createdNode;

		public int NodeIndex { get; private set; }

		public bool IsPath
		{
			get => isPath;
			set
			{
				isPath = value;
				SetNode();
			}
		}

		public void Init(Map map, NodesPool pool, int index)
		{
			this.map = map;
			NodeIndex = index;
			nodesPool = pool;

			SetNode();
		}

		public void ToggleNodeType()
		{
			var newType = map.GetNode(NodeIndex).type == NodeType.Traversable ? NodeType.Obstacle : NodeType.Traversable;
			map.ChangeNodeType(NodeIndex, newType);
			SetNode();
		}

		private void OnDestroy()
		{
			nodesPool.ReturnNode(currentType, createdNode);
		}

		private void SetNode()
		{
			nodesPool.ReturnNode(currentType, createdNode);
			
			if (isPath)
				currentType = NodeObjectType.Path;
			else
				currentType = map.GetNode(NodeIndex).type == NodeType.Traversable
					? NodeObjectType.Traversable
					: NodeObjectType.Obstacle;
			createdNode = nodesPool.GetNode(currentType);

			var createdTransform = createdNode.transform;
			createdTransform.SetParent(transform);
			createdTransform.localPosition = Vector3.zero;
		}
	}
}