using ITUTest.Pathfinding;
using UnityEngine;

namespace ITUTest
{
    public class NodeGameObject : MonoBehaviour
    {
        private NodesPool nodesPool;

        private GameObject createdNode;
        private GameObject createdPath;
        
        public void Init(Map map, NodesPool pool, Node node)
        {
            nodesPool = pool;
            
            createdNode = node.type == NodeType.Traversable ? nodesPool.GetNode() : nodesPool.GetObstacle();
            createdNode.transform.SetParent(transform);
            createdNode.transform.localPosition = Vector3.zero;
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
    }
}
