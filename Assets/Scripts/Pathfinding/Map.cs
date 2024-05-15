using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace ITUTest.Pathfinding
{
    public struct Map
    {
        public Node[,] nodes;
        
        public int Width { get; }
        public int Height { get; }

        public Map(int width, int height)
        {
            nodes = new Node[width, height];
            Width = width;
            Height = height;
            
            GenerateRandomMap();
        }

        public void GetNearbyNodes(Node node, ref List<Node> nearbyNodes)
        {
            nearbyNodes.Clear();
            
            if(node.position.y - 1 >= 0)
                nearbyNodes.Add(nodes[node.position.x, node.position.y - 1]);
            if(node.position.y + 1 < Height)
                nearbyNodes.Add(nodes[node.position.x, node.position.y + 1]);
            if(node.position.x - 1 >= 0)
                nearbyNodes.Add(nodes[node.position.x - 1, node.position.y]);
            if(node.position.x + 1 < Width)
                nearbyNodes.Add(nodes[node.position.x + 1, node.position.y]);
        }

        public Node GetRandomNode()
        {
            while (true)
            {
                var x = Random.Range(0, Width);
                var y = Random.Range(0, Height);
                if (nodes[x, y].type == NodeType.Traversable)
                {
                    return nodes[x, y];
                }
            }
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    stringBuilder.Append((int)nodes[x, y].type);
                    stringBuilder.Append(", ");
                }
                stringBuilder.Append("\n");
            }

            return stringBuilder.ToString();
        }

        // Random Distribution of obstacles. Only checks if at least 2 nodes are traversable (for testing)
        private void GenerateRandomMap()
        {
            int traversableCount;
            do
            {
                traversableCount = 0;
                for (int x = 0; x < Width; x++)
                {
                    for (int y = 0; y < Height; y++)
                    {
                        var type = Random.Range(0f, 1f) < 0.5f ? NodeType.Obstacle : NodeType.Traversable;
                        if (type == NodeType.Traversable)
                            traversableCount += 1;
                        nodes[x, y] = new Node(new(x, y), type);
                    }
                }
            } while (traversableCount < 2);
            
        }
    }
}
