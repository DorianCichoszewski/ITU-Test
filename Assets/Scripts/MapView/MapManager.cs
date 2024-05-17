using ITUTest.Pathfinding;
using UnityEngine;

namespace ITUTest.MapView
{
	public class MapManager : MonoBehaviour
	{
		[SerializeField]
		private NodesPool nodesPool;

		[SerializeField]
		private MapDisplay mapDisplay;

		[SerializeField]
		private ModelOnPath modelController;

		[SerializeField]
		private PathOnMap pathOnMap;
		
		private Map generatedMap;
		private PathScene currentPath;

		public Map GeneratedMap => generatedMap;

		public void GenerateMap(int width, int height, MapGenerationMode mode)
		{
			generatedMap = new Map(width, height, mode);
			mapDisplay.DisplayMap(generatedMap, nodesPool);
			pathOnMap.ResetMap(generatedMap);
			currentPath = null;
		}
		
		public Node GetNode(NodeObject nodeObject)
		{
			return generatedMap.GetNode(nodeObject.NodeIndex);
		}
		
		public NodeObject GetNodeObject(Node node)
		{
			return mapDisplay.GetNodeObject(generatedMap.GetIndex(node));
		}

		public void UpdatePath(Path newPath)
		{
			var newPathScene = new PathScene(newPath, this);
			if (currentPath == newPathScene)
				return;
			mapDisplay.UpdateNodesOnPath(currentPath, newPathScene);
			modelController.SetNewPath(newPathScene);
			currentPath = newPathScene;
		}
	}
}