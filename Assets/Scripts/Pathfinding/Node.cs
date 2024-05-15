using UnityEngine;

namespace ITUTest.Pathfinding
{
    public struct Node
    {
        public readonly Vector2Int position;
        public readonly NodeType type;

        public Node(Vector2Int position, NodeType type)
        {
            this.position = position;
            this.type = type;
        }
    }
}
