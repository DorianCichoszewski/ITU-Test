using System.Text;
using ITUTest.Pathfinding;
using UnityEngine;

namespace ITUTest
{
	public class MapConsoleLogger : MonoBehaviour
	{
		[SerializeField]
		private int width, height;

		[ContextMenu("Generate map")]
		private void Start()
		{
			var map = new Map(width, height);
			PrintMap(map);
		}

		public void PrintMap(Map map)
		{
			var stringBuilder = new StringBuilder();

			for (int x = 0; x < map.Width; x++)
			{
				for (int y = 0; y < map.Height; y++)
				{
					stringBuilder.Append((int)map.nodes[x, y].type);
					stringBuilder.Append(", ");
				}
				stringBuilder.Append("\n");
			}

			Debug.Log(stringBuilder.ToString());
		}
	}
}