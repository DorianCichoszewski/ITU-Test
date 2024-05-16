using System;
using UnityEngine;

namespace ITUTest
{
	[RequireComponent(typeof(Camera))]
	public class CameraRaycast : MonoBehaviour
	{
		private new Camera camera;

		private void Start()
		{
			camera = GetComponent<Camera>();
		}

		private void Update()
		{
			if (!Input.GetMouseButtonDown(0))
				return;
			
			RaycastHit hit;
			Ray ray = camera.ScreenPointToRay(Input.mousePosition);
			
			if (Physics.Raycast(ray, out hit))
			{
				var component = hit.transform.GetComponentInParent<ICameraInputTaker>();
				if (component != null)
					component.MousePressed();
			}
		}
	}
}