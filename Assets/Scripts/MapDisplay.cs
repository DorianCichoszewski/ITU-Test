using System;
using ITUTest.Pathfinding;
using UnityEngine;

namespace ITUTest
{
    public class MapDisplay : MonoBehaviour
    {
        [SerializeField]
        private int mapWidth, mapHeight;

        [SerializeField]
        private float nodeDistance = 1f;

        [SerializeField]
        private GameObject nodePrefab;

        [SerializeField]
        private GameObject nodeObstaclePrefab;

        [SerializeField]
        private Transform nodesParentTransform;
        
        private Map map;

        private void Start()
        {
            GenerateMap();
        }

        public void GenerateMap()
        {
            map = new Map(mapWidth, mapHeight);
            
            float startX = -mapWidth * nodeDistance / 2f;
            float startY = -mapHeight * nodeDistance / 2f;

            for (int i = 0; i < map.Width; i++)
            {
                for (int j = 0; j < map.Height; j++)
                {
                    var node = map.GetNode(i, j);
                    Vector3 position = new (startX + i * nodeDistance, 0, startY + j * nodeDistance);
                    var prefab = node.type == NodeType.Traversable ? nodePrefab : nodeObstaclePrefab;
                    Instantiate(prefab, position, Quaternion.identity, nodesParentTransform);
                }
            }
        }
    }
}
