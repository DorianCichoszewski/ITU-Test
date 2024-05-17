using System.Collections.Generic;
using System.Linq;
using StarterAssets;
using UnityEngine;

namespace ITUTest.MapView
{
	public class ModelOnPath : MonoBehaviour
	{
		[SerializeField]
		private ModelController controller;

		private IEnumerator<NodeObject> nodesEnumerator;

		private void Awake()
		{
			DisableController();
		}

		public void SetNewPath(PathScene path)
		{
			if (path is not { nodes: not null })
			{
				DisableController();
				return;
			}

			controller.gameObject.SetActive(true);
			controller.transform.position = path.start.transform.position;
			nodesEnumerator = path.nodes.AsEnumerable().GetEnumerator();
			AssignNewNode();
		}

		private void AssignNewNode()
		{
			if (!nodesEnumerator.MoveNext())
			{
				DisableController();
				return;
			}
			
			controller.SetTargetPosition(nodesEnumerator.Current.transform.position, AssignNewNode);
		}

		private void DisableController()
		{
			controller.SetTargetPosition(Vector3.zero, null);
			controller.gameObject.SetActive(false);
		}
	}
}