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

        private void GenerateRandomMap()
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    var type = Random.Range(0f, 1f) < 0.5f ? NodeType.Obstacle : NodeType.Traversable;
                    nodes[x, y] = new Node(new(x, y), type);
                }
            }
        }
    }
}
