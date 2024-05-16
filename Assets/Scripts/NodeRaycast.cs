using System;
using ITUTest.MapView;
using UnityEngine;

namespace ITUTest
{
	[RequireComponent(typeof(Camera))]
	public class NodeRaycast : MonoBehaviour
	{
		private new Camera camera;

		public event Action<NodeObject> OnNodeSelected;

		private void Start()
		{
			camera = GetComponent<Camera>();
		}

		private void Update()
		{
			if (!Input.GetMouseButtonDown(0) || OnNodeSelected == null)
				return;

			RaycastHit hit;
			var ray = camera.ScreenPointToRay(Input.mousePosition);

			if (Physics.Raycast(ray, out hit))
			{
				var component = hit.transform.GetComponentInParent<NodeObject>();
				if (component != null)
					OnNodeSelected?.Invoke(component);
			}
		}
	}
}