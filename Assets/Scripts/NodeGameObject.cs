using ITUTest.Pathfinding;
using UnityEngine;

namespace ITUTest
{
    public class NodeGameObject : MonoBehaviour
    {
        private Map map;
        private NodesPool nodesPool;
        private Vector2Int position;

        private GameObject createdNode;
        
        public void Init(Map map, NodesPool pool, int x, int y)
        {
            this.map = map;
            nodesPool = pool;
            position = new Vector2Int(x, y);
            
            var node = map.GetNode(x, y);
            createdNode = node.type == NodeType.Traversable ? nodesPool.GetNode() : nodesPool.GetObstacle();
            createdNode.transform.SetParent(transform);
            createdNode.transform.localPosition = Vector3.zero;
        }
    }
}
